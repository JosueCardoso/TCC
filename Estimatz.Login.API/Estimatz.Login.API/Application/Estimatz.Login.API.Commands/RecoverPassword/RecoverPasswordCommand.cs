using MediatR;

namespace Estimatz.Login.API.Commands.RecoverPassword
{
    public class RecoverPasswordCommand : IRequest
    {
        public string Email { get; set; }
        public string? ApplicationURL { get; set; }
    }
}
