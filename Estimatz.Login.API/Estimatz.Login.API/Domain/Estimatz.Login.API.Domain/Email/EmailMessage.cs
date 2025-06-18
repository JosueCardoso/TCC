using MimeKit;

namespace Estimatz.Login.API.Entities.Email
{
    public class EmailMessage
    {
        public TextPart Body { get; set; }
        public string Subject { get; set; }
        public MailboxAddress Recipient { get; set; }
    }
}
