using MimeKit;

namespace Estimatz.Entities.Email
{
    public class EmailMessage
    {
        public TextPart Body { get; set; }
        public string Subject { get; set; }
        public MailboxAddress Recipient { get; set; }
    }
}
