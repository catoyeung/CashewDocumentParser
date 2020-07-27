using System.Net.Mail;
using CashewDocumentParser.API.Configurations;

namespace CashewDocumentParser.API.Helpers
{
    public interface IEmailSender
    {
        bool SendRegisterVerificationEmail(string username, string toEmail, string confirmationLink);
        bool SendPasswordResetEmail(string username, string toEmail, string resetPasswordLink);
    }

    public class EmailSender : IEmailSender
    {
        private EmailSender _instance { get; set; }
        private EmailConfiguration _emailConfiguration { get; set; }
        public EmailSender(EmailConfiguration emailConfiguration)
        {
            _emailConfiguration = emailConfiguration;
        }

        public bool SendRegisterVerificationEmail(string username, string toEmail, string verificationLink)
        {
            MailMessage msg = new MailMessage();
            SmtpClient client = new SmtpClient();

            msg.Subject = "[KYOCERA Form Xtractor] Please verify email address";
            msg.Body = $@"<html>
                            <body>
                                <p>Hi, {username}</p>
                                <p><a href='{verificationLink}'>Please click here to verify your email: {toEmail}</p>
                            </body>
                        </html>";
            msg.From = new MailAddress(_emailConfiguration.From);
            msg.To.Add(toEmail);
            msg.IsBodyHtml = true;
            client.Host = _emailConfiguration.SmtpServer;
            System.Net.NetworkCredential basicauthenticationinfo = new System.Net.NetworkCredential(_emailConfiguration.Username, _emailConfiguration.Password);
            client.Port = _emailConfiguration.Port;
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = basicauthenticationinfo;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.Send(msg);

            return true;
        }

        public bool SendPasswordResetEmail(string username, string toEmail, string resetPasswordLink)
        {
            MailMessage msg = new MailMessage();
            SmtpClient client = new SmtpClient();

            msg.Subject = "[KYOCERA Form Xtractor] Please reset password";
            msg.Body = $@"<html>
                            <body>
                                <p>Hi, {username}</p>
                                <p>We just received your request to reset password. Please click <a href='{resetPasswordLink}'>here</a> to reset your password./p>
                            </body>
                        </html>";
            msg.From = new MailAddress(_emailConfiguration.From);
            msg.To.Add(toEmail);
            msg.IsBodyHtml = true;
            client.Host = _emailConfiguration.SmtpServer;
            System.Net.NetworkCredential basicauthenticationinfo = new System.Net.NetworkCredential(_emailConfiguration.Username, _emailConfiguration.Password);
            client.Port = _emailConfiguration.Port;
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = basicauthenticationinfo;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.Send(msg);

            return true;
        }
    }
}
