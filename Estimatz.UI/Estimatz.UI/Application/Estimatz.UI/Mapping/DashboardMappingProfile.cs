using AutoMapper;
using Estimatz.UI.Commands.CreateRoom;
using Estimatz.UI.Entities.Indicators;
using Estimatz.UI.Models;

namespace Estimatz.UI.Mapping
{
    public class DashboardMappingProfile : Profile
    {
        public DashboardMappingProfile()
        {
            CreateMap<Indicator, IndicatorModel>();
        }
    }
}
