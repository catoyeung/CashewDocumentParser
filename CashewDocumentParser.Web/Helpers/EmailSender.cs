using System.Net.Mail;
using CashewDocumentParser.Web.Configurations;

namespace CashewDocumentParser.Web.Helpers
{
    public interface IEmailSender
    {
        bool SendRegisterConfirmationEmail(string username, string toEmail, string confirmationLink);
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

        public bool SendRegisterConfirmationEmail(string username, string toEmail, string confirmationLink)
        {
            MailMessage msg = new MailMessage();
            SmtpClient client = new SmtpClient();

            msg.Subject = "[CY三國殺]請確認電郵地址";
            msg.Body = $@"<html>
                            <body>
                                <p>你好，{username}</p>
                                <p><a href='{confirmationLink}'>請按此確認登記電郵為{toEmail}</p>
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

            msg.Subject = "[CY三國殺]請重新設定密碼";
            msg.Body = $@"<html>
                            <body>
                                <p>你好，{username}</p>
                                <p><a href='{resetPasswordLink}'>收到重設密碼的要求，請按此重設密碼。</p>
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
