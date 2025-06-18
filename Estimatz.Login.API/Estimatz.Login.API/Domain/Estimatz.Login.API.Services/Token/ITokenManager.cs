using Estimatz.Login.API.Entities.Token;
using Estimatz.Login.API.Entities.User;

namespace Estimatz.Login.API.Services.Token
{
    public interface ITokenManager
    {
        Task<SignInUser> GenerateToken(string email);
        Task<SignInUser> RefreshToken(RefreshTokenRequestModel refreshTokenModel);
        bool IsValidToken(string token);
        void InvalidToken(string token);
        Task<string> GenerateConfirmEmailTokenAsync(ApplicationUser user);
        Task<string> GenerateRecoverPasswordToken(ApplicationUser user);
        SimpleToken GetSimpleToken(string token);
    }
}
