using Estimatz.UI.Entities.Room;
using Estimatz.UI.ExternalServices.EstimatzApi;
using MediatR;

namespace Estimatz.UI.Queries.GetRoomsByUserId
{
	public class GetRoomByUserQueryHandler : IRequestHandler<GetRoomByUserQuery, List<SimpleRoom>>
	{
		private readonly IEstimatzApiClient _estimatzApiClient;

        public GetRoomByUserQueryHandler(IEstimatzApiClient estimatzApiClient)
		{
			_estimatzApiClient = estimatzApiClient;	
		}

		public async Task<List<SimpleRoom>> Handle(GetRoomByUserQuery request, CancellationToken cancellationToken)
		{
			var response = await _estimatzApiClient.GetAllSimpleRooms(request.UserId);
			return response.Data;
        }
	}
}
