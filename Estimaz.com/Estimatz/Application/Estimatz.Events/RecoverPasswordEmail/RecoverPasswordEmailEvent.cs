using MediatR;

namespace Estimatz.Events.RecoverPasswordEmail
{
    public class RecoverPasswordEmailEvent : INotification
    {
        public string Email { get; set; }
        public string Username { get; set; }
        public string UserId { get; set; }
        public string Token { get; set; }
        public string ApplicationURL { get; set; }
    }
}
