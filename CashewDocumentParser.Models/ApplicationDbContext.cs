using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CashewDocumentParser.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>(b =>
            {
                b.ToTable("TB_Users");
            });

            modelBuilder.Entity<IdentityUserLogin<string>>(b =>
            {
                b.ToTable("TB_UserLogin");
            });

            modelBuilder.Entity<IdentityUserToken<string>>(b =>
            {
                b.ToTable("TB_UserToken");
            });

            modelBuilder.Entity<IdentityRole>(b =>
            {
                b.ToTable("TB_Roles");
            });

            modelBuilder.Entity<IdentityUserRole<string>>(b =>
            {
                b.ToTable("TB_UserRole");
            });

            modelBuilder.Entity<IdentityUserClaim<string>>(b =>
            {
                b.ToTable("TB_UserClaim");
            });

            modelBuilder.Entity<IdentityRoleClaim<string>>(b =>
            {
                b.ToTable("TB_RoleClaim");
            });

            modelBuilder.Entity<Template>(b =>
            {
                b.ToTable("TB_Template");
            });

            modelBuilder.Entity<ClassificationQueue>(b =>
            {
                b.ToTable("TB_ClassificationQueue");
            });

            modelBuilder.Entity<ExtractQueue>(b =>
            {
                b.ToTable("TB_ExtractQueue");
            });

            modelBuilder.Entity<ImportQueue>(b =>
            {
                b.ToTable("TB_ImportQueue");
            });

            modelBuilder.Entity<OCRQueue>(b =>
            {
                b.ToTable("TB_OCRQueue");
            });

            modelBuilder.Entity<PreprocessingQueue>(b =>
            {
                b.ToTable("TB_PreprocessingQueue");
            });

            modelBuilder.Entity<ScriptingQueue>(b =>
            {
                b.ToTable("TB_ScriptingQueue");
            });

            modelBuilder.Entity<IntegrationQueue>(b =>
            {
                b.ToTable("TB_IntegrationQueue");
            });

            modelBuilder.Entity<ProcessedQueue>(b =>
            {
                b.ToTable("TB_ProcessedQueue");
            });
        }
    }
}