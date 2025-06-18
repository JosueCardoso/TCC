using MediatR;

namespace Estimatz.Login.API.Commands.ConfirmEmail
{
    public class ConfirmEmailCommand : IRequest
    {
        public string UserId { get; set; }
        public string Token { get; set; }
    }
}
