using Estimatz.Entities.User;
using MediatR;

namespace Estimatz.Commands.Account.RefreshToken
{
    public class RefreshTokenCommand : IRequest<SignInUser>
    {
        public string Email { get; set; }
        public string TokenString { get; set; }
    }
}
