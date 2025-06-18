using Estimatz.UI.Entities.Indicators;
using Estimatz.UI.ExternalServices.EstimatzApi;
using MediatR;

namespace Estimatz.UI.Queries.GetIndicators
{
    public class GetIndicatorsQueryHandler : IRequestHandler<GetIndicatorsQuery, List<Indicator>>
    {
        private readonly IEstimatzApiClient _estimatzApiClient;

        public GetIndicatorsQueryHandler(IEstimatzApiClient estimatzApiClient)
        {
            _estimatzApiClient = estimatzApiClient;
        }

        public async Task<List<Indicator>> Handle(GetIndicatorsQuery request, CancellationToken cancellationToken)
        {
            var response = await _estimatzApiClient.GetIndicators(request.UserId);
            return response.Data;
        }
    }
}
