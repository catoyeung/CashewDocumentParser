using System.ComponentModel.DataAnnotations;

namespace CashewDocumentParser.ViewModels.Forms
{
    public class ResetPasswordForm
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string Token { get; set; }

        public ResetPasswordForm()
        {
        }
    }
}
