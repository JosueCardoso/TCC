using Estimatz.UI.Entities.User;
using Estimatz.UI.ExternalServices.EstimatzLoginApi.Common;

namespace Estimatz.UI.ExternalServices.EstimatzLoginApi.Login
{
    public class LoginResponse : CommonResponse
    {                
        public SignInUser Data { get; set; }
    }
}
