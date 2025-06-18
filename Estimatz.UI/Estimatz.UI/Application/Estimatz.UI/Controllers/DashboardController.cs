using AutoMapper;
using Estimatz.UI.Extensions;
using Estimatz.UI.Models;
using Estimatz.UI.Queries.GetIndicators;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Estimatz.UI.Controllers
{
    public class DashboardController : BaseController
    {
        public DashboardController(IMapper mapper, IMediator mediator, IHttpContextAccessor httpContextAccessor) : base(mapper, mediator, httpContextAccessor){}

        public async Task<IActionResult> Dashboard()
        {
            var isValidUser = await IsValidUser();
            if (!isValidUser)
                return RedirectToAction("Index", "Base");

            MenuManager.SetMenuActive("dashboard");

            var userIdString = _session.GetString("userId");
            var response = await _mediatr.Send(new GetIndicatorsQuery { UserId = Guid.Parse(userIdString) });

            var model = new DashboardModel();
            response.ForEach(x => model.Indicators.Add(_mapper.Map<IndicatorModel>(x)));

            return View("Pages/Dashboard.cshtml", model);
        }
    }
}
