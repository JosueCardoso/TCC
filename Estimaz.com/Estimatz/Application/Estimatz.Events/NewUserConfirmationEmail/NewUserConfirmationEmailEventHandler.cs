using Estimatz.Entities.Email;
using Estimatz.Infra.Services.EmailService;
using MediatR;
using Microsoft.Extensions.Logging;
using MimeKit;
using System.Net;
using System.Text.Encodings.Web;

namespace Estimatz.Events.NewUserConfirmationEmail
{
    public class NewUserConfirmationEmailEventHandler : INotificationHandler<NewUserConfirmationEmailEvent>
    {
        private readonly IEmailService _emailService;
        private readonly ILogger<NewUserConfirmationEmailEventHandler> _logger;

        public NewUserConfirmationEmailEventHandler(IEmailService emailService, ILogger<NewUserConfirmationEmailEventHandler> logger)
        {
            _emailService = emailService;
            _logger = logger;
        }

        public Task Handle(NewUserConfirmationEmailEvent notification, CancellationToken cancellationToken)
        {
            var urlDefault = "https://3.94.190.185/Account/ConfirmCreateAccount"; //TODO: Encontrar uma forma de deixar dinamica essa parada
            var url = string.IsNullOrEmpty(notification.ApplicationURL) ? urlDefault : notification.ApplicationURL;

            _emailService.SendEmail(new EmailMessage 
            {
                Subject = "[Estimatz] Ativação de conta",
                Recipient = new MailboxAddress(notification.Username, notification.Email),
                Body = new TextPart("html")
                {
                    Text = CreateEmailBody(notification.Username, CreateConfirmationUrl(notification.UserId, notification.Token, url))
                }
            });

            _logger.LogInformation($"Email de confirmação para criação de conta enviado para {notification.Email}");
            return Task.CompletedTask;
        }

        private string CreateConfirmationUrl(string userId, string token, string url)
        {            
            return $"{url}?userId={userId}&token={WebUtility.UrlEncode(token)}";
        }

        private string CreateEmailBody(string username, string link)
        {
            return @$"<html>
                      <body>
                        <p style=""text-align:center; font-size: 22px;"">Confirmação de e-mail</p></br>
                        <p style=""text-align:center"">Seja bem-vindo(a), {username}! Sua conta foi criado com sucesso.</p>
                        <p style=""text-align:center"">Para confirmar seu e-mail, basta clicar no botão abaixo.</p>
                        <p style=""text-align:center""><a href=""{HtmlEncoder.Default.Encode(link)}"">Confirmar E-mail<a/></p>
                        <p style=""text-align:center""><a href=""#"">Estimatz.com<a/> | 2023</p>
                      </body>
                      </html>";
        }
    }
}
