using Estimatz.Login.API.Entities.Email;

namespace Estimatz.Login.API.Infra.Services.EmailService
{
    public interface IEmailService
    {
        void SendEmail(EmailMessage emailContent);
    }
}
