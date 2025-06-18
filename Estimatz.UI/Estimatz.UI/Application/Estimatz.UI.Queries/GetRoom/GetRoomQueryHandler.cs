using Estimatz.UI.Entities.Room;
using Estimatz.UI.ExternalServices.EstimatzApi;
using MediatR;

namespace Estimatz.UI.Queries.GetRoom
{
	public class GetRoomQueryHandler : IRequestHandler<GetRoomQuery, Room>
	{
        private readonly IEstimatzApiClient _estimatzApiClient;

		public GetRoomQueryHandler(IEstimatzApiClient estimatzApiClient)
		{
			_estimatzApiClient = estimatzApiClient;
        }

        public async Task<Room> Handle(GetRoomQuery request, CancellationToken cancellationToken)
		{
			var response = await _estimatzApiClient.GetRoom(request.RoomId, request.UserId);
			return response.Data;
        }
	}
}
