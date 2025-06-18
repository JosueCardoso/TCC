using MediatR;

namespace Estimatz.UI.Commands.DeleteRoom
{
	public class DeleteRoomCommand : IRequest<bool>
	{
		public Guid UserId { get; set; }
		public Guid RoomId { get; set; }
	}
}
