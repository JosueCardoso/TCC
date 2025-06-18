using Estimatz.UI.ExternalServices.EstimatzLoginApi.Common;
using Estimatz.UI.ExternalServices.EstimatzLoginApi.Login;
using Estimatz.UI.ExternalServices.EstimatzLoginApi.RecoverPassword;
using Estimatz.UI.ExternalServices.EstimatzLoginApi.Register;

namespace Estimatz.UI.ExternalServices.EstimatzLoginApi
{
    public interface ILoginApiClient
    {
        Task<CommonResponse> Register(RegisterRequest request);
        Task<CommonResponse> ConfirmEmail(string userId, string token);
        Task<CommonResponse> RecoverPassword(RecoverPasswordRequest request);
        Task<CommonResponse> ConfirmRecoverPassword(ConfirmRecoverPasswordRequest request);
        Task<LoginResponse> Login(LoginRequest request);
        Task<CommonResponse> ValidateUser(string userId, string token);
    }
}
