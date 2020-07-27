using System;
namespace CashewDocumentParser.ViewModels.Forms
{
    public class SignInForm
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
        public SignInForm()
        {
        }
    }
}
