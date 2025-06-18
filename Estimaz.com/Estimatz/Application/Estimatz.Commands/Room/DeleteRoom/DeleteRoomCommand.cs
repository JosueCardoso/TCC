using MediatR;

namespace Estimatz.Commands.Room.DeleteRoom
{
    public class DeleteRoomCommand : IRequest
    {
        public Guid RoomId { get; set; }
        public Guid UserId { get; set; }
    }
}
