using MediatR;

namespace Estimatz.Queries.Account.ValidateUser
{
    public class ValidateUserQuery : IRequest
    {
        public string UserId { get; set; }
        public string Token { get; set; }
    }
}
