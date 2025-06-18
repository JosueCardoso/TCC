using AutoMapper;
using Estimatz.UI.ExternalServices.EstimatzApi;
using Estimatz.UI.ExternalServices.EstimatzApi.AddStory;
using MediatR;

namespace Estimatz.UI.Commands.AddStory
{
    public class AddStoryCommandHandler : IRequestHandler<AddStoryCommand, bool>
    {
        private readonly IEstimatzApiClient _estimatzApiClient;
        private readonly IMapper _mapper;

        public AddStoryCommandHandler(IEstimatzApiClient estimatzApiClient, IMapper mapper)
        {
            _estimatzApiClient = estimatzApiClient;
            _mapper = mapper;
        }

        public async Task<bool> Handle(AddStoryCommand command, CancellationToken cancellationToken)
        {
            var request = _mapper.Map<AddStoryRequest>(command);
            var response = await _estimatzApiClient.AddStory(request);

            return response.Success;
        }
    }
}
