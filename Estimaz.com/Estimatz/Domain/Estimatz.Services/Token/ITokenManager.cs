using Estimatz.Entities.Token;
using Estimatz.Entities.User;

namespace Estimatz.Services.Token
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
