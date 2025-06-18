using MediatR;

namespace Estimatz.Events.NewUserConfirmationEmail
{
    public class NewUserConfirmationEmailEvent : INotification
    {
        public string Email { get; set; }
        public string Username { get; set; }
        public string UserId { get; set; }
        public string Token { get; set; }
        public string ApplicationURL { get; set; }
    }
}
