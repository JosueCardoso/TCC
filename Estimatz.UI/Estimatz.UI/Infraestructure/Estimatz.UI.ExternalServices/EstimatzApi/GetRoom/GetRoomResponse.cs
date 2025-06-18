using Estimatz.UI.Entities.Room;
using Estimatz.UI.ExternalServices.EstimatzLoginApi.Common;

namespace Estimatz.UI.ExternalServices.EstimatzApi.GetRoom
{
	public class GetRoomResponse : CommonResponse
    {
		public Room Data { get; set; }
	}
}
