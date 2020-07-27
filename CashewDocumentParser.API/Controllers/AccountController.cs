using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using CashewDocumentParser.Models;
using CashewDocumentParser.ViewModels.Forms;
using CashewDocumentParser.API.Helpers;
using Microsoft.AspNetCore.Cors;

namespace CashewDocumentParser.API.Controllers
{
    [EnableCors("AllowOrigins")]
    [Route("api/Account")]
    public class AccountController : Controller
    {
        private IConfiguration _config { get; set; }
        private SignInManager<ApplicationUser> _signInManager { get; set; }
        private UserManager<ApplicationUser> _userManager { get; set; }
        private ILogger<AccountController> _logger { get; set; }
        private IEmailSender _emailSender { get; set; }

        public AccountController(
            IConfiguration config,
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            ILogger<AccountController> logger,
            IEmailSender emailSender)
        {
            _config = config;
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
            _emailSender = emailSender;
        }

        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUpAsync([FromBody]SignUpForm signUpForm)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser {
                    FirstName = signUpForm.FirstName,
                    LastName = signUpForm.LastName,
                    UserName = signUpForm.Email.Split("@")[0],
                    Email = signUpForm.Email
                };
                var result = await _userManager.CreateAsync(user, signUpForm.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User has completed user registration.");

                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var encodedToken = HttpUtility.UrlEncode(token);

                    var verificationLink = String.Format($"{signUpForm.VerifyEmailLink}?token={encodedToken}&email={user.Email}");

                    _emailSender.SendRegisterVerificationEmail(user.FirstName + " " + user.LastName, user.Email, verificationLink);

                    return Ok(new { user });
                } else
                {
                    if (result.Errors.Count() > 0)
                    {
                        return BadRequest(new { errorMessage = result.Errors.First().Description });
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return BadRequest();
        }

        [HttpPost("SignIn")]
        public async Task<IActionResult> SignInAsync([FromBody]SignInForm signInForm)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(signInForm.Email);

                if (!await _userManager.CheckPasswordAsync(user, signInForm.Password))
                {
                    return BadRequest(new { errorMessage = "Username Or password is not correct." });
                }

                Microsoft.AspNetCore.Identity.SignInResult result = null;
                result = await _signInManager.PasswordSignInAsync(user.UserName,
                                   signInForm.Password, signInForm.RememberMe, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    var tokenHandler = new JwtSecurityTokenHandler();

                    var key = Encoding.ASCII.GetBytes(_config["IdentitySetting:Secret"]);
                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new Claim[]
                        {
                            new Claim(ClaimTypes.Name, user.Id.ToString())
                        }),
                        Expires = DateTime.UtcNow.AddDays(7),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                    };
                    var token = tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));

                    // Write token to http only cookie
                    Response.Cookies.Append(
                        "access_token",
                        token,
                        new CookieOptions()
                        {
                            HttpOnly = true,
                            SameSite = SameSiteMode.None,
                            Secure = true
                        }
                    );

                    return Ok(new { token, user });
                }
                if (result.IsLockedOut)
                {
                    return BadRequest(new { errorMessage = "User account is locked." });
                }
                if (result.IsNotAllowed)
                {
                    return BadRequest(new { errorMessage = "User account is not allowed." });
                }
            }
            return BadRequest(new { errorMessage = "Please contact system administrator." });
        }

        [Authorize]
        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePasswordAsync([FromBody]ChangePasswordForm changePasswordForm)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(changePasswordForm.Email);

                Microsoft.AspNetCore.Identity.SignInResult signInResult = null;
                try
                {
                    signInResult = await _signInManager.PasswordSignInAsync(user.UserName,
                                   changePasswordForm.OldPassword, true, lockoutOnFailure: false);
                }
                catch
                {
                    return BadRequest(new { errorMessage = "Username or password is not correct." });
                }
                if (signInResult.IsLockedOut)
                {
                    return BadRequest(new { errorMessage = "User account is locked." });
                }
                if (signInResult.IsNotAllowed)
                {
                    return BadRequest(new { errorMessage = "User account is not allowed." });
                }
                if (signInResult.Succeeded)
                {
                    user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, changePasswordForm.NewPassword);
                    var result = await _userManager.UpdateAsync(user);
                    if (!result.Succeeded)
                    {
                        return BadRequest(new { errorMessage = "Change password has been failed" });
                    }
                    return Ok();
                }
            }
            return BadRequest(new { errorMessage = "Please contact system administrator." });
        }

        [HttpGet("AccessDenied")]
        public IActionResult AccessDenied()
        {
            return BadRequest(new { errorMessage = "You are not authroized to access this page." });
        }

        [Authorize]
        [HttpPost("SignOut")]
        public async Task<IActionResult> SignOutAsync()
        {
            await _signInManager.SignOutAsync();
            return Ok();
        }

        [HttpPost("VerifyEmail")]
        public async Task<IActionResult> VerifyEmailAsync([FromBody]VerifyEmailForm verifyEmailForm)
        {
            var user = await _userManager.FindByEmailAsync(verifyEmailForm.Email);
            if (user == null)
                return BadRequest(new { errorMessage = "User account has not been registered." });

            var result = await _userManager.ConfirmEmailAsync(user, verifyEmailForm.Token);

            if (!result.Succeeded) return BadRequest(new { errorMessage = "Please contact system administrator." });

            return Ok();
        }

        [HttpPost("ForgetPassword")]
        public async Task<IActionResult> ForgetPassword([FromBody]ForgotPasswordForm forgotPasswordForm)
        {
            if (!ModelState.IsValid)
                return BadRequest(new
                {
                    errorMessage = ModelState.Values
                                        .SelectMany(x => x.Errors)
                                        .Select(x => x.ErrorMessage)
                });

            var user = await _userManager.FindByEmailAsync(forgotPasswordForm.Email);
            if (user == null)
                return BadRequest(new { errorMessage = "Email has not been registered." });

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var encodedToken = HttpUtility.UrlEncode(token);

            //var resetPasswordLink = String.Format($"{Request.Scheme}://{Request.Host}/api/Account/ResetPassword?token={encodedToken}&email={user.Email}");
            var resetPasswordLink = String.Format($"{forgotPasswordForm.ResetPasswordLink}?token={encodedToken}&email={user.Email}");

            _emailSender.SendPasswordResetEmail(user.FirstName + " " + user.LastName , forgotPasswordForm.Email, resetPasswordLink);

            return Ok();
        }

        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody]ResetPasswordForm resetPasswordForm)
        {
            if (!ModelState.IsValid)
                return BadRequest(new
                {
                    errorMessage = ModelState.Values
                                        .SelectMany(x => x.Errors)
                                        .Select(x => x.ErrorMessage)
                });

            var user = await _userManager.FindByEmailAsync(resetPasswordForm.Email);
            if (user == null)
                return BadRequest(new { errorMessage = "Email has been registered." });

            var resetPassResult = await _userManager.ResetPasswordAsync(user, resetPasswordForm.Token, resetPasswordForm.Password);
            if (!resetPassResult.Succeeded)
            {
                return BadRequest(new
                {
                    errorMessage = resetPassResult.Errors
                                        .Select(x => x.Description)
                });
            }

            return Ok();
        }
    }
}
