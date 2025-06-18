using MediatR;

namespace Estimatz.Login.API.Queries.ValidateUser
{
    public class ValidateUserQuery : IRequest
    {
        public string UserId { get; set; }
        public string Token { get; set; }
    }
}
