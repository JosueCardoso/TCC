using MediatR;

namespace Estimatz.Commands.Account.RecoverPassword
{
    public class RecoverPasswordCommand : IRequest
    {
        public string Email { get; set; }
        public string? ApplicationURL { get; set; }
    }
}
