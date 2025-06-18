using Estimatz.UI.ExternalServices.EstimatzApi;
using MediatR;

namespace Estimatz.UI.Commands.CreateRoom
{
	public class CreateRoomCommandHandler : IRequestHandler<CreateRoomCommand, Guid>
	{
        private readonly IEstimatzApiClient _estimatzApiClient;
		public CreateRoomCommandHandler(IEstimatzApiClient estimatzApiClient)
		{
			_estimatzApiClient = estimatzApiClient;
		}

        public async Task<Guid> Handle(CreateRoomCommand request, CancellationToken cancellationToken)
		{
			return await _estimatzApiClient.CreateRoom(request);
		}
	}
}
