using MediatR;

namespace Estimatz.UI.Queries.ValidateUser
{
    public class ValidateUserQuery : IRequest<bool>
    {
        public string UserId { get; set; }
        public string Token { get; set; }
    }
}
