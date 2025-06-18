using Estimatz.Login.API.Commands.RefreshToken;
using Estimatz.Login.API.Notifications;
using Estimatz.Login.API.Queries.ValidateUser;
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
    public class TokenController : MainController
    {
        private readonly IMediator _mediator;

        public TokenController(IMediator mediator, INotificator notificator) : base(notificator)
        {
            _mediator = mediator;
        }

        [HttpGet("refresh-token")]
        [Authorize]
        public async Task<ActionResult> RefreshToken([FromQuery][Required] string email)
        {
            var accessToken = await HttpContext.GetTokenAsync("Bearer", "access_token");
            var request = new RefreshTokenCommand { TokenString = accessToken, Email = email };

            return CustomResponse(await _mediator.Send(request));
        }

        [HttpGet("validate-user")]
        public async Task<ActionResult> ValidateUser([FromQuery][Required] string userId, [FromQuery][Required] string token)
        {
            await _mediator.Send(new ValidateUserQuery { UserId = userId, Token = token });
            return CustomResponse();
        }
    }
}
