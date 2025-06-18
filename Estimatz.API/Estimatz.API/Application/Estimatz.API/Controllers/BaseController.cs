using Estimatz.API.Models;
using Estimatz.API.Notifications;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Estimatz.API.Controllers
{
    [ApiController]
    public class BaseController : Controller
    {
        private readonly INotificator _notificator;
        protected readonly IMediator _mediatr;

        public BaseController(INotificator notificator, IMediator mediatr)
        {
            _notificator = notificator;
            _mediatr = mediatr;
        }

        protected ActionResult CustomResponse(object result = null)
        {
            if (_notificator.IsSucess && _notificator.HasMessages)
                return Ok(new ActionResponse(success: true, messages: _notificator.Messages.Select(x => x)));
            else if (_notificator.IsSucess)
                return Ok(new ActionResponse(success: true, data: result, messages: null));

            return BadRequest(new ActionResponse(success: false, messages: _notificator.Messages.Select(x => x)));
        }
    }
}
