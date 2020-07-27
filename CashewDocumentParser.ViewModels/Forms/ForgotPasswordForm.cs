using System.ComponentModel.DataAnnotations;

namespace CashewDocumentParser.ViewModels.Forms
{
    public class ForgotPasswordForm
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string ResetPasswordLink { get; set; }

        public ForgotPasswordForm()
        {
        }
    }
}
