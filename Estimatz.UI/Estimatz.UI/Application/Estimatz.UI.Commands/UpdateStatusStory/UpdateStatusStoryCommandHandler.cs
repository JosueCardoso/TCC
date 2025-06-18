using AutoMapper;
using Estimatz.UI.ExternalServices.EstimatzApi;
using Estimatz.UI.ExternalServices.EstimatzApi.UpdateStoryStatus;
using MediatR;

namespace Estimatz.UI.Commands.UpdateStatusStory
{
    public class UpdateStatusStoryCommandHandler : IRequestHandler<UpdateStatusStoryCommand, bool>
    {
        private readonly IEstimatzApiClient _estimatzApiClient;
        private readonly IMapper _mapper;

        public UpdateStatusStoryCommandHandler(IEstimatzApiClient estimatzApiClient, IMapper mapper)
        {
            _estimatzApiClient = estimatzApiClient;
            _mapper = mapper;
        }

        public async Task<bool> Handle(UpdateStatusStoryCommand command, CancellationToken cancellationToken)
        {
            var request = _mapper.Map<UpdateStoryStatusRequest>(command);
            var response = await _estimatzApiClient.UpdateStoryStatus(request);

            return response.Success;
        }
    }
}
