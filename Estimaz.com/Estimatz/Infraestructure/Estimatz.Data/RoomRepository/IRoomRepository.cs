using Estimatz.Entities.Room;
using Microsoft.Azure.Cosmos;

namespace Estimatz.Data.RoomRepository
{
    public interface IRoomRepository
    {
        Task<ItemResponse<Room>> CreateRoom(Room room);
        Task<Room> FindRoom(Guid id);
        List<SimpleRoom> GetAllRoomByUserId(Guid userId);
        Task<ItemResponse<Room>> DeleteRoom(Guid id);
        Task<ItemResponse<Room>> UpdateStatusRoom(Guid roomId, RoomStatus newRoomStatus);
    }
}
