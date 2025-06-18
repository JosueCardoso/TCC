using Estimatz.UI.Entities.Story;
using Estimatz.UI.ExternalServices.EstimatzApi;
using MediatR;

namespace Estimatz.UI.Queries.GetStory
{
    public class GetStoryQueryHandler : IRequestHandler<GetStoryQuery, UserStory>
    {
        private readonly IEstimatzApiClient _estimatzApiClient;

        public GetStoryQueryHandler(IEstimatzApiClient estimatzApiClient)
        {
            _estimatzApiClient = estimatzApiClient;
        }

        public async Task<UserStory> Handle(GetStoryQuery request, CancellationToken cancellationToken)
        {
            var response = await _estimatzApiClient.GetStory(request.RoomId, request.StoryId);
            return response.Data;
        }
    }
}
