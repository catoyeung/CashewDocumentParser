using System;
using System.Text;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.Cookies;
using CashewDocumentParser.Models;
using CashewDocumentParser.Web.Configurations;
using CashewDocumentParser.Web.Helpers;
using CashewDocumentParser.Models.Infrastructure;
using CashewDocumentParser.Models.Repositories;
using CashewDocumentParser.Services;
using Hangfire;
using CashewDocumentParser.Web.Middlewares;

namespace CashewDocumentParser.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        private void ConfigureEntityFramework(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
            Configuration.GetConnectionString("DefaultConnection")));
            services.AddIdentity<ApplicationUser, IdentityRole>().
                AddEntityFrameworkStores<ApplicationDbContext>().
                AddDefaultTokenProviders();
        }

        private void ConfigureIdentity(IServiceCollection services)
        {
            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 0;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 0;
                options.Lockout.AllowedForNewUsers = true;

                // User settings.
                options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = true;
            });

            var key = Encoding.ASCII.GetBytes(Configuration["IdentitySetting:Secret"]);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
        }

        private void ConfigureCookies(IServiceCollection services)
        {
            services.ConfigureApplicationCookie(options =>
            {
                options.ExpireTimeSpan = TimeSpan.FromMinutes(5);

                options.LoginPath = "/account/signin";
                options.AccessDeniedPath = "/account/accessdenied";
                options.SlidingExpiration = true;

                options.Events = new CookieAuthenticationEvents
                {
                    OnRedirectToLogin = context =>
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        return Task.FromResult(0);
                    }
                };
            });
        }

        private void ConfigureEmail(IServiceCollection services)
        {
            var emailConfig = Configuration
                .GetSection("EmailConfiguration")
                .Get<EmailConfiguration>();
            services.AddSingleton(emailConfig);
            services.AddScoped<IEmailSender, EmailSender>();
        }

        private void ConfigureDependencyInjection(IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IClassificationQueueRepository, ClassificationQueueRepository>();
            services.AddScoped<IExtractQueueRepository, ExtractQueueRepository>();
            services.AddScoped<IImportQueueRepository, ImportQueueRepository>();
            services.AddScoped<IIntegrationQueueRepository, IntegrationQueueRepository>();
            services.AddScoped<IOCRQueueRepository, OCRQueueRepository>();
            services.AddScoped<IPreprocessingQueueRepository, PreprocessingQueueRepository>();
            services.AddScoped<IProcessedQueueRepository, ProcessedQueueRepository>();
            services.AddScoped<IScriptingQueueRepository, ScriptingQueueRepository>();
            services.AddScoped<ITemplateRepository, TemplateRepository>();

            services.AddScoped<IQueueService, QueueService>();
        }

        private void ConfigureHangfire(IServiceCollection services)
        {
            // Add Hangfire services.
            services.AddHangfire(configuration => configuration
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseSqlServerStorage(Configuration.GetConnectionString("DefaultConnection")));

            // Add the processing server as IHostedService
            services.AddHangfireServer();
        }

        private void ConfigureBackgroundJobs(IServiceProvider serviceProvider)
        {
            var queueService = serviceProvider.GetRequiredService<IQueueService>();
            RecurringJob.AddOrUpdate<IQueueService>(queueService => queueService.MoveFromProcessedToImport(), Cron.Minutely);
            RecurringJob.AddOrUpdate<IQueueService>(queueService => queueService.MoveFromImportToOCR(), Cron.Minutely);
            RecurringJob.AddOrUpdate<IQueueService>(queueService => queueService.ProcessOCRQueue(), Cron.Minutely);
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureEntityFramework(services);

            ConfigureIdentity(services);

            ConfigureCookies(services);

            ConfigureEmail(services);

            ConfigureDependencyInjection(services);

            ConfigureHangfire(services);

            services.AddCors(options =>
            {
                options.AddPolicy(
                    "AllowOrigins",
                    builder =>
                    {
                        builder.SetIsOriginAllowed(host => true)
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials();
                    });
            });

            services.AddControllersWithViews();

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app,
            IBackgroundJobClient backgroundJobs,
            IWebHostEnvironment env,
            IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseRouting();

            app.UseHangfireDashboard();
            app.UseHangfireServer(new BackgroundJobServerOptions
            {
                HeartbeatInterval = new TimeSpan(0, 0, 5),
                ServerCheckInterval = new TimeSpan(0, 0, 5),
                SchedulePollingInterval = new TimeSpan(0, 0, 5)
            });

            ConfigureBackgroundJobs(serviceProvider);

            app.UseMiddleware<JWTMiddleware>();

            app.UseCors("AllowOrigins");

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });
        }
    }
}
