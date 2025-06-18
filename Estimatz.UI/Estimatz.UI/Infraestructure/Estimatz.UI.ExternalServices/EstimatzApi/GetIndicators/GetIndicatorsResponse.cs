using Estimatz.UI.Entities.Indicators;
using Estimatz.UI.ExternalServices.EstimatzLoginApi.Common;

namespace Estimatz.UI.ExternalServices.EstimatzApi.GetIndicators
{
    public class GetIndicatorsResponse : CommonResponse
    {
        public List<Indicator> Data { get; set; }
    }
}
