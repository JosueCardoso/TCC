using MediatR;

namespace Estimatz.Login.API.Commands.Logout
{
    public class LogoutCommand : IRequest
    {
        public string Token { get; set; }
    }
}
