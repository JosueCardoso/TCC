using MailKit.Net.Smtp;
using MimeKit;
using MailKit.Security;
using Estimatz.Entities.Email;

namespace Estimatz.Infra.Services.EmailService
{
    public class MailKitEmailService : IEmailService
    {
        public void SendEmail(EmailMessage emailContent)
        {
            var smtpClient = new SmtpClient();
            smtpClient.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            smtpClient.Authenticate("register.estimatz@gmail.com", "pnzdfkqjwrmgyepa"); //Estimatz@06052023

            var message = new MimeMessage();

            message.From.Add(new MailboxAddress("Estimatz Register", "register.estimatz@gmail.com")); //TODO: Esse e-mail pode ser separado para cada coisa (recuperação de senha, criação de conta e etc)
            message.To.Add(emailContent.Recipient);
            message.Subject = emailContent.Subject;
            message.Body = emailContent.Body;

            smtpClient.Send(message);
            smtpClient.Disconnect(true);
        }
    }
}
