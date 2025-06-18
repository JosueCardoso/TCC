using AutoMapper;
using Estimatz.UI.ExternalServices.EstimatzApi;
using Estimatz.UI.ExternalServices.EstimatzApi.RemoveStory;
using MediatR;

namespace Estimatz.UI.Commands.RemoveStory
{
    public class RemoveStoryCommandHandler : IRequestHandler<RemoveStoryCommand, bool>
    {
        private readonly IEstimatzApiClient _estimatzApiClient;
        private readonly IMapper _mapper;

        public RemoveStoryCommandHandler(IEstimatzApiClient estimatzApiClient, IMapper mapper)
        {
            _estimatzApiClient = estimatzApiClient;
            _mapper = mapper;
        }

        public async Task<bool> Handle(RemoveStoryCommand command, CancellationToken cancellationToken)
        {
            var request = _mapper.Map<RemoveStoryRequest>(command);
            var response = await _estimatzApiClient.RemoveStory(request);

            return response.Success;
        }
    }
}
