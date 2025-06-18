using Estimatz.Login.API.Commands.ConfirmEmail;
using Estimatz.Login.API.Commands.ConfirmRecoverPassword;
using Estimatz.Login.API.Commands.Logout;
using Estimatz.Login.API.Commands.RecoverPassword;
using Estimatz.Login.API.Commands.RefreshToken;
using Estimatz.Login.API.Commands.Register;
using Estimatz.Login.API.Notifications;
using Estimatz.Login.API.Queries.SignIn;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Estimatz.Login.API.Controllers.v1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class AccountController : MainController
    {
        private readonly IMediator _mediator;

        public AccountController(IMediator mediator, INotificator notificator) : base(notificator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register(RegisterNewUserCommand request)
        {
            await _mediator.Send(request);
            return CustomResponse();
        }

        [HttpGet("confirm-email")]
        public async Task<ActionResult> ConfirmEmail([FromQuery][Required]string userId, [FromQuery][Required] string token)
        {
            await _mediator.Send(new ConfirmEmailCommand { Token = token, UserId = userId });
            return CustomResponse();
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(SignInQuery request)
        {
            return CustomResponse(await _mediator.Send(request));
        }
                
        [HttpPost("logout")]
        [Authorize]
        public async Task<ActionResult> Logout()
        {
            var accessToken = await HttpContext.GetTokenAsync("Bearer", "access_token");
            await _mediator.Send(new LogoutCommand { Token = accessToken });

            return CustomResponse();
        }

        [HttpPost("recover-password")]
        public async Task<ActionResult> RecoverPassword(RecoverPasswordCommand request)
        {
            await _mediator.Send(request);
            return CustomResponse();
        }

        [HttpPost("confirm-recover-password")]
        public async Task<ActionResult> ConfirmRecoverPassword(ConfirmRecoverPasswordCommand request)
        {
            await _mediator.Send(request);
            return CustomResponse();
        }
    }
}
