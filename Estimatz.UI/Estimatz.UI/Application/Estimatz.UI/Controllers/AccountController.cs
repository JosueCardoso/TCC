using AutoMapper;
using Estimatz.UI.Commands.ConfirmEmailSignIn;
using Estimatz.UI.Commands.ConfirmRecoverPassword;
using Estimatz.UI.Commands.RecoverPassword;
using Estimatz.UI.Commands.SignIn;
using Estimatz.UI.Models;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Estimatz.UI.Queries.Login;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Newtonsoft.Json;

namespace Estimatz.UI.Controllers
{
    public class AccountController : BaseController
    {
        public AccountController(IMapper mapper, IMediator mediator, IHttpContextAccessor httpContextAccessor) : base(mapper, mediator, httpContextAccessor)    
        {}

        [HttpPost]
        public async Task<IActionResult> CreateAccount(CreateAccountModel model)
        {
            var command = _mapper.Map<SignInCommand>(model);
            var response = await _mediatr.Send(command);

            return Json(new { success = response.Success, messages = response.Messages.Select(x => x)});
        }
        
        [HttpGet]
        public async Task<IActionResult> ConfirmCreateAccount([FromQuery][Required] string userId, [FromQuery][Required] string token)
        {
            var response = await _mediatr.Send(new ConfirmEmailSignInCommand { UserId = userId, Token = token });
            var model = _mapper.Map<CommonModel>(response);

            return View("Pages/EmailAccountConfirmation.cshtml", model);
        }

        [HttpPost]
        public async Task<IActionResult> SendEmailPasswordRecovery(string email)
        {
            var response = await _mediatr.Send(new RecoverPasswordCommand { Email = email });
            return Json(new { success = response.Success, messages = response.Messages.Select(x => x) });
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
            var response = await _mediatr.Send(command);

            return Json(new { success = response.Success, messages = response.Messages.Select(x => x) });
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            var query = _mapper.Map<LoginQuery>(model);
            var response = await _mediatr.Send(query);

            if (!response.Success)
                return Json(new { success = response.Success, messages = response.Messages.Select(x => x) });

            var authenticationProperties = new AuthenticationProperties
            {
                IsPersistent = true, // Define se o cookie deve ser persistente entre sessões
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(response.SignInUser.ExpiresIn) // Tempo de expiração do cookie
            };
            
            var claims = new List<Claim>();            
            foreach (var userClaim in response.SignInUser?.UserToken?.Claims)
            {
                claims.Add(new Claim(userClaim.Type, userClaim.Value));
            }

            var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(claims, JwtBearerDefaults.AuthenticationScheme));
            _httpContextAccessor.HttpContext.User = claimsPrincipal;
            //await _httpContextAccessor.HttpContext.SignInAsync(claimsPrincipal); //TODO: Achar um jeito de arrumar essa bagaça... Não estaá armazenando o usuário

            Response.Cookies.Append("accessToken", response.SignInUser?.AccessToken, new CookieOptions
            {
                Secure = true,
                Expires = authenticationProperties.ExpiresUtc, // Tempo de expiração do cookie
                HttpOnly = true // Acessível apenas via HTTP
            });

            _session.SetString("username", _httpContextAccessor.HttpContext.User.Identity.Name);
            _session.SetString("userId", response.SignInUser.UserToken.Id);

            return Json(new { success = _httpContextAccessor.HttpContext.User.Identity.IsAuthenticated, messages = response.Messages.Select(x => x) });
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
