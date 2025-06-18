using MediatR;

namespace Estimatz.Commands.Account.Logout
{
    public class LogoutCommand : IRequest
    {
        public string Token { get; set; }
    }
}
