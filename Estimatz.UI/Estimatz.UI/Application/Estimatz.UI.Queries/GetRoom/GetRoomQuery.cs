using Estimatz.UI.Entities.Room;
using MediatR;

namespace Estimatz.UI.Queries.GetRoom
{
	public class GetRoomQuery : IRequest<Room>
	{
		public Guid UserId { get; set; }
		public Guid RoomId { get; set; }
	}
}
