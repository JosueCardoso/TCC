using Estimatz.UI.Entities.Room;
using Estimatz.UI.ExternalServices.EstimatzLoginApi.Common;

namespace Estimatz.UI.ExternalServices.EstimatzApi.GetAllSimpleRooms
{
	public class GetAllSimpleRoomsResponse : CommonResponse
    {
        public List<SimpleRoom> Data { get; set; }
    }
}
