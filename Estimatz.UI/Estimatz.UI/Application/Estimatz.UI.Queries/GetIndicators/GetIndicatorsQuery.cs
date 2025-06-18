using Estimatz.UI.Entities.Indicators;
using MediatR;

namespace Estimatz.UI.Queries.GetIndicators
{
    public class GetIndicatorsQuery : IRequest<List<Indicator>>
    {
        public Guid UserId { get; set; }
    }
}
