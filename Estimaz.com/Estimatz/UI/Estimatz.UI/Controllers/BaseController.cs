using AutoMapper;
using Estimatz.Notifications;
using Estimatz.Queries.Account.ValidateUser;
using Estimatz.Resources;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace Estimatz.UI.Controllers
{
    public class BaseController : Controller
    {
        protected readonly IMapper _mapper;
        protected readonly IMediator _mediatr;
        protected readonly ISession _session;
        protected readonly IHttpContextAccessor _httpContextAccessor;
        protected readonly INotificator _notificator;

        public BaseController(IMapper mapper, IMediator mediator, IHttpContextAccessor httpContextAccessor, INotificator notificator)
        {
            _mapper = mapper;
            _mediatr = mediator;
            _httpContextAccessor = httpContextAccessor;
            _session = httpContextAccessor.HttpContext.Session;
            _notificator = notificator;
        }

        public ActionResult Index()
        {
            _session.SetString("username", String.Empty);
            _session.SetString("userId", String.Empty);

            return View("Pages/Index.cshtml");
        }

        [HttpPost]
        public void SwitchLanguage(string culture)
        {
            Language.SetCulture(culture);
        }

        public async Task<bool> IsValidUser()
        {
            var cookies = _httpContextAccessor.HttpContext.Request.Cookies;

            var userId = _session.Get("userId");
            var accessToken = cookies["accessToken"];

            if (userId != null && !string.IsNullOrEmpty(accessToken))
            {
                await _mediatr.Send(new ValidateUserQuery { UserId = Encoding.UTF8.GetString(userId), Token = accessToken });
                return _notificator.IsSuccess;
            }

            return false;
        }
    }
}
