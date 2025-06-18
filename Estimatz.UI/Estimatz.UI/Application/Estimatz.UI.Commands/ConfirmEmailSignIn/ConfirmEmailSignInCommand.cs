using Estimatz.UI.Commands.Common;
using MediatR;

namespace Estimatz.UI.Commands.ConfirmEmailSignIn
{
    public class ConfirmEmailSignInCommand : IRequest<CommonCommandResponse>
    {
        public string UserId { get; set; }
        public string Token { get; set; }
    }
}
