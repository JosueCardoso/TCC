using Estimatz.UI.Commands.Common;
using MediatR;

namespace Estimatz.UI.Commands.ConfirmRecoverPassword
{
    public class ConfirmRecoverPasswordCommand : IRequest<CommonCommandResponse>
    {
        public string UserId { get; set; }
        public string PasswordRecoveryToken { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
