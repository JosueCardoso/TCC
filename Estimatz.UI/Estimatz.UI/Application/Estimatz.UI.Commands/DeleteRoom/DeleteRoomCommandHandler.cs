using Estimatz.UI.ExternalServices.EstimatzApi;
using MediatR;

namespace Estimatz.UI.Commands.DeleteRoom
{
	public class DeleteRoomCommandHandler : IRequestHandler<DeleteRoomCommand, bool>
	{
        private readonly IEstimatzApiClient _estimatzApiClient;
		public DeleteRoomCommandHandler(IEstimatzApiClient estimatzApiClient)
		{
			_estimatzApiClient = estimatzApiClient;
		}

        public async Task<bool> Handle(DeleteRoomCommand request, CancellationToken cancellationToken)
		{
			var response = await _estimatzApiClient.DeleteRoom(request.RoomId, request.UserId);
			return response.Success;
        }
	}
}
