using Estimatz.Cache.TokenCache;
using Estimatz.Entities.AppConfig;
using Estimatz.Entities.Token;
using Estimatz.Entities.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Claim = System.Security.Claims.Claim;
using ClaimUser = Estimatz.Entities.User.Claim;

namespace Estimatz.Services.Token
{
    public class TokenManager : ITokenManager
    {
        private readonly TokenConfiguration _appSettings;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITokenMemoryCache _tokenCache;
                
        public TokenManager(IOptions<TokenConfiguration> appSettings, UserManager<ApplicationUser> userManager, ITokenMemoryCache tokenCache)
        {
            _appSettings = appSettings.Value;
            _userManager = userManager;
            _tokenCache = tokenCache;
        }

        public async Task<SignInUser> GenerateToken(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var claims = await _userManager.GetClaimsAsync(user);
            var userRoles = await _userManager.GetRolesAsync(user);

            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, ToUnixEpochDate(DateTime.UtcNow).ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(), ClaimValueTypes.Integer64));
            claims.Add(new Claim(ClaimTypes.Authentication, "true"));
            claims.Add(new Claim(ClaimTypes.Name, user.Name));

            foreach (var userRole in userRoles)
            {
                claims.Add(new Claim("role", userRole));
            }

            var identityClaims = new ClaimsIdentity();
            identityClaims.AddClaims(claims);

            var expireAt = DateTime.UtcNow.AddMinutes(_appSettings.Expiration);
            var accessToken = CreateToken(identityClaims, expireAt);
            var simpleToken = new SimpleToken
            {
                ExpireAt = expireAt,
                UserId = user.Id,
                TokenString = accessToken
            };

            _tokenCache.Add(accessToken, simpleToken);

            var response = new SignInUser
            {
                AccessToken = accessToken,
                ExpiresIn = TimeSpan.FromMinutes(_appSettings.Expiration).TotalSeconds,
                UserToken = new UserToken
                {
                    Id = user.Id,
                    Email = user.Email,
                    Claims = claims.Select(x => new ClaimUser
                    {
                        Type = x.Type,
                        Value = x.Value,
                    }),
                }
            };

            return response;
        }

        public bool IsValidToken(string token)
        {
            if (string.IsNullOrEmpty(token))
                return false;

            var tokenByCache = _tokenCache.Get(token);
            if (tokenByCache is null || tokenByCache.ExpireAt < DateTime.UtcNow)
                return false;

            return true;
        }

        public async Task<SignInUser> RefreshToken(RefreshTokenRequestModel refreshTokenModel)
        {
            var newToken = await GenerateToken(refreshTokenModel.Email);
            _tokenCache.Remove(refreshTokenModel.TokenString);

            return newToken;
        }

        public void InvalidToken(string token)
        {
            _tokenCache.Remove(token);
        }

        public async Task<string> GenerateConfirmEmailTokenAsync(ApplicationUser user)
        {
            var emailConfirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var expireAt = DateTime.UtcNow.AddMinutes(30);
            var simpleToken = new SimpleToken
            {
                ExpireAt = expireAt,
                UserId = user.Id,
                TokenString = emailConfirmationToken
            };

            _tokenCache.Add(emailConfirmationToken, simpleToken);

            return emailConfirmationToken;
        }

        public async Task<string> GenerateRecoverPasswordToken(ApplicationUser user)
        {
            var passwordResetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
            var expireAt = DateTime.UtcNow.AddMinutes(30);
            var simpleToken = new SimpleToken
            {
                ExpireAt = expireAt,
                UserId = user.Id,
                TokenString = passwordResetToken
            };

            _tokenCache.Add(passwordResetToken, simpleToken);

            return passwordResetToken;
        }

        public SimpleToken GetSimpleToken(string token) => _tokenCache.Get(token);

        private string CreateToken(ClaimsIdentity claims, DateTime expireAt)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);

            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _appSettings.Emissor,
                Audience = _appSettings.Validate,
                Subject = claims,
                Expires = expireAt,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            });

            return tokenHandler.WriteToken(token);
        }

        private static long ToUnixEpochDate(DateTime date)
           => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);
    }
}
