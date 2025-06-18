using Estimatz.Entities.Email;
using Estimatz.Infra.Services.EmailService;
using MediatR;
using Microsoft.Extensions.Logging;
using MimeKit;
using System.Net;
using System.Text.Encodings.Web;

namespace Estimatz.Events.RecoverPasswordEmail
{
    public class RecoverPasswordEmailEventHandler : INotificationHandler<RecoverPasswordEmailEvent>
    {
        private readonly IEmailService _emailService;
        private readonly ILogger<RecoverPasswordEmailEventHandler> _logger;

        public RecoverPasswordEmailEventHandler(IEmailService emailService, ILogger<RecoverPasswordEmailEventHandler> logger)
        {
            _emailService = emailService;
            _logger = logger;
        }

        public Task Handle(RecoverPasswordEmailEvent notification, CancellationToken cancellationToken)
        {
            var urlDefault = "https://3.94.190.185/api/v1/Account/confirm-recover-password"; //Esse endpoint não funciona com GET, dessa forma, a URL padrão nunca vai funcionar
            var url = string.IsNullOrEmpty(notification.ApplicationURL) ? urlDefault : notification.ApplicationURL;

            _emailService.SendEmail(new EmailMessage
            {
                Subject = "[Estimatz] Recuperação de senha",
                Recipient = new MailboxAddress(notification.Username, notification.Email),
                Body = new TextPart("html")
                {
                    Text = CreateEmailBody(notification.Username, CreateConfirmationUrl(notification.UserId, notification.Token, url))
                }
            });

            _logger.LogInformation($"Email de recuperação de senha enviado com sucesso para {notification.Email}");
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
                        <p style=""text-align:center; font-size: 22px;"">Recuperação de senha</p></br>
                        <p style=""text-align:center"">Olá, {username}! Recebemos uma solicitação para restaurar sua senha de acesso em nosso site.</p>
                        <p style=""text-align:center"">Ela ocorreu em { DateTime.Now }</p>
                        <p style=""text-align:center"">Se você reconhece essa ação, clique no botão abaixo para prosseguir:</p>
                        <p style=""text-align:center""><a href=""{HtmlEncoder.Default.Encode(link)}"">Redefinir senha<a/></p>
                        <p style=""text-align:center""><a href=""#"">Estimatz.com<a/> | 2023</p>
                      </body>
                      </html>";
        }
    }
}
