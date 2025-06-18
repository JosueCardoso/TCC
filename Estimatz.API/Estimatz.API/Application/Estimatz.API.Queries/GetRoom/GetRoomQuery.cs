using Estimatz.API.Entities.Room;
using MediatR;

namespace Estimatz.API.Queries.GetRoom
{
    public class GetRoomQuery : IRequest<Room>
    {
        public Guid RoomId { get; set; }
        public Guid UserId { get; set; }
    }
}
