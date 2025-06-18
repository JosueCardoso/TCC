using Estimatz.UI.Entities.Room;
using MediatR;

namespace Estimatz.UI.Commands.CreateRoom
{
	public class CreateRoomCommand : Room, IRequest<Guid>
	{
	}
}
