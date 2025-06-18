using Estimatz.Login.API.Models;
using Estimatz.Login.API.Notifications;
using Microsoft.AspNetCore.Mvc;

namespace Estimatz.Login.API.Controllers
{
    [ApiController]
    public class MainController : ControllerBase
    {
        private readonly INotificator _notificator;

        public MainController(INotificator notificator)
        {
            _notificator = notificator;
        }

        protected ActionResult CustomResponse(object result = null)
        {
            if (_notificator.IsSucess && _notificator.HasMessages)
                return Ok(new ActionResponse(success: true, messages: _notificator.Messages.Select(x => x)));
            else if(_notificator.IsSucess)
                return Ok(new ActionResponse(success: true, data: result, messages: null));

            return BadRequest(new ActionResponse(success: false, messages: _notificator.Messages.Select(x => x)));
        }
    }
}
