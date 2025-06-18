using MediatR;

namespace Estimatz.UI.Queries.Login
{
    public class LoginQuery : IRequest<LoginQueryResponse>
    {
        public string Password { get; set; }
        public string Email { get; set; }
    }
}
