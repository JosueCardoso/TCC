using Estimatz.Entities.Email;

namespace Estimatz.Infra.Services.EmailService
{
    public interface IEmailService
    {
        void SendEmail(EmailMessage emailContent);
    }
}
