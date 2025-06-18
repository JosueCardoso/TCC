using AutoMapper;
using Estimatz.Entities.Indicators;
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
