using Estimatz.Entities.Indicators;
using MediatR;

namespace Estimatz.API.Queries.GetIndicators
{
    public class GetIndicatorsQuery : IRequest<List<Indicator>>
    {
        public Guid UserId { get; set; }
    }
}
