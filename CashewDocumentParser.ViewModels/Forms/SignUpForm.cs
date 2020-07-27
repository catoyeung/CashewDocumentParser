using System;
namespace CashewDocumentParser.ViewModels.Forms
{
    public class SignUpForm
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string VerifyEmailLink { get; set; }
        public SignUpForm()
        {
        }
    }
}
