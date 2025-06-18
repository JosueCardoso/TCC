using AutoMapper;
using Estimatz.Commands.Account.ConfirmEmail;
using Estimatz.Commands.Account.ConfirmRecoverPassword;
using Estimatz.Commands.Account.RecoverPassword;
using Estimatz.Commands.Account.Register;
using Estimatz.Notifications;
using Estimatz.Queries.Account.SignIn;
using Estimatz.UI.Models;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Estimatz.UI.Controllers
{
    public class AccountController : BaseController
    {
        public AccountController(IMapper mapper, IMediator mediator, IHttpContextAccessor httpContextAccessor, INotificator notificator) : base(mapper, mediator, httpContextAccessor, notificator) {}

        [HttpPost]
        public async Task<IActionResult> CreateAccount(CreateAccountModel model)
        {
            var command = _mapper.Map<RegisterNewUserCommand>(model);
            await _mediatr.Send(command);

            return Json(new { success = _notificator.IsSuccess, messages = _notificator.Messages.Select(x => x) });
        }
               
        public async Task<IActionResult> ConfirmCreateAccount([FromQuery][Required] string userId, [FromQuery][Required] string token)
        {
            await _mediatr.Send(new ConfirmEmailCommand { UserId = userId, Token = token });
			var model = new CommonModel { Success = _notificator.IsSuccess, Messages = _notificator.Messages.Select(x => x.Description).ToList() }; 

            return View("Pages/EmailAccountConfirmation.cshtml", model);
        }

        [HttpPost]
        public async Task<IActionResult> SendEmailPasswordRecovery(string email)
        {
            await _mediatr.Send(new RecoverPasswordCommand { Email = email });
            return Json(new { success = _notificator.IsSuccess, messages = _notificator.Messages.Select(x => x) });
        }

        [HttpGet]
        public IActionResult ValidatePasswordRecoveryToken([FromQuery][Required] string userId, [FromQuery][Required] string token)
        {
            var model = new ValidatePasswordRecoveryTokenModel { UserId = userId, PasswordRecoveryToken = token };
            return View("Pages/Partial/_RecoverPasswordSecondStep.cshtml", model);
        }

        [HttpPost]
        public async Task<IActionResult> RecoverPassword(ValidatePasswordRecoveryTokenModel model)
        {
            var command = _mapper.Map<ConfirmRecoverPasswordCommand>(model);
            await _mediatr.Send(command);

            return Json(new { success = _notificator.IsSuccess, messages = _notificator.Messages.Select(x => x) });
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            var query = _mapper.Map<SignInQuery>(model);
            var response = await _mediatr.Send(query);

            if (!_notificator.IsSuccess)
                return Json(new { success = _notificator.IsSuccess, messages = _notificator.Messages.Select(x => x) });

            var authenticationProperties = new AuthenticationProperties
            {
                IsPersistent = true, // Define se o cookie deve ser persistente entre sessões
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(response.ExpiresIn) // Tempo de expiração do cookie
            };

            var claims = new List<Claim>();
            foreach (var userClaim in response.UserToken?.Claims)
            {
                claims.Add(new Claim(userClaim.Type, userClaim.Value));
            }

            var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(claims, JwtBearerDefaults.AuthenticationScheme));
            _httpContextAccessor.HttpContext.User = claimsPrincipal;
            //await _httpContextAccessor.HttpContext.SignInAsync(claimsPrincipal); //TODO: Achar um jeito de arrumar essa bagaça... Não estaá armazenando o usuário

            Response.Cookies.Append("accessToken", response.AccessToken, new CookieOptions
            {
                Secure = true,
                Expires = authenticationProperties.ExpiresUtc, // Tempo de expiração do cookie
                HttpOnly = true // Acessível apenas via HTTP
            });

            _session.SetString("username", _httpContextAccessor.HttpContext.User.Identity.Name);
            _session.SetString("userId", response.UserToken.Id);

            return Json(new { success = _httpContextAccessor.HttpContext.User.Identity.IsAuthenticated, messages = _notificator.Messages.Select(x => x) });
        }

        public async Task<IActionResult> Logout()
        {
            return View("/Pages/Index.cshtml");
        }

        [HttpPost]
        public IActionResult CreateQuickUser(string name)
        {
            _session.SetString("username", name);

            return Json(new { success = true });
        }
    }
}
