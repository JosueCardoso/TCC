using MediatR;

namespace Estimatz.API.Commands.DeleteRoom
{
    public class DeleteRoomCommand : IRequest
    {
        public Guid RoomId { get; set; }
        public Guid UserId { get; set; }
    }
}
