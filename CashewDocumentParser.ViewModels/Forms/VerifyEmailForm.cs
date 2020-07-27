using System;
namespace CashewDocumentParser.ViewModels.Forms
{
    public class VerifyEmailForm
    {
        public string Email { get; set; }
        public string Token { get; set; }
        public VerifyEmailForm()
        {
        }
    }
}
