using Estimatz.Login.API.Entities.User;
using MediatR;

namespace Estimatz.Login.API.Commands.RefreshToken
{
    public class RefreshTokenCommand : IRequest<SignInUser>
    {
        public string Email { get; set; }
        public string TokenString { get; set; }
    }
}
