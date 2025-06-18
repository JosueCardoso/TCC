using MediatR;

namespace Estimatz.Commands.Account.ConfirmEmail
{
    public class ConfirmEmailCommand : IRequest
    {
        public string UserId { get; set; }
        public string Token { get; set; }
    }
}
