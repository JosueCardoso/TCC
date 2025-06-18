using Estimatz.UI.Entities.User;

namespace Estimatz.UI.Queries.Login
{
    public class LoginQueryResponse
    {
        public SignInUser SignInUser { get; set; }
        public bool Success { get; set; }
        public List<string> Messages { get; set; }
    }
}
