using Estimatz.Entities.User;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NSubstitute;

namespace Estimatz.Test.Fakes
{
    public class FakeSignInManager : SignInManager<ApplicationUser>
    {
        #region Fields
        private readonly bool _simulateSuccess = false;
        #endregion

        #region Constructors
        public FakeSignInManager(UserManager<ApplicationUser> userManager, bool simulateSuccess = true)
                : base(userManager,
                     Substitute.For<IHttpContextAccessor>(),
                     Substitute.For<IUserClaimsPrincipalFactory<ApplicationUser>>(),
                     Substitute.For<IOptions<IdentityOptions>>(),
                     Substitute.For<ILogger<SignInManager<ApplicationUser>>>(),
                     Substitute.For<IAuthenticationSchemeProvider>())
        {
            this._simulateSuccess = simulateSuccess;
        }
        #endregion

        #region Public methods
        public override Task<SignInResult> PasswordSignInAsync(ApplicationUser user, string password, bool isPersistent, bool lockoutOnFailure)
        {
            return this.ReturnResult(this._simulateSuccess);
        }

        public override Task<SignInResult> PasswordSignInAsync(string userName, string password, bool isPersistent, bool lockoutOnFailure)
        {
            return this.ReturnResult(this._simulateSuccess);
        }

        public override Task<SignInResult> CheckPasswordSignInAsync(ApplicationUser user, string password, bool lockoutOnFailure)
        {
            return this.ReturnResult(this._simulateSuccess);
        }
        #endregion

        #region Internal methods
        private Task<SignInResult> ReturnResult(bool isSuccess = true)
        {
            SignInResult result = SignInResult.Success;

            if (!isSuccess)
                result = SignInResult.Failed;

            return Task.FromResult(result);
        }
        #endregion
    }
}
