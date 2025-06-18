using Estimatz.UI.Commands.Common;
using MediatR;

namespace Estimatz.UI.Commands.RecoverPassword
{
    public class RecoverPasswordCommand : IRequest<CommonCommandResponse>
    {
        public string Email { get; set; }
    }
}
