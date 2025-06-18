using Estimatz.UI.Commands.Common;
using MediatR;

namespace Estimatz.UI.Commands.SignIn
{
    public class SignInCommand : IRequest<CommonCommandResponse>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
