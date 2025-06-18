using MediatR;

namespace Estimatz.Login.API.Commands.ConfirmRecoverPassword
{
    public class ConfirmRecoverPasswordCommand : IRequest
    {
        public string UserId { get; set; }
        public string Token { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
