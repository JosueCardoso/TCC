using Estimatz.API.Entities.Room;
using MediatR;

namespace Estimatz.API.Queries.GetAllRooms
{
    public class GetSimpleRoomsQuery : IRequest<List<SimpleRoom>>
    {
        public Guid UserId { get; set; }
    }
}
