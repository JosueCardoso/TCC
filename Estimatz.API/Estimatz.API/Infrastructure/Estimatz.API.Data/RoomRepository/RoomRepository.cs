using Estimatz.API.CosmosDB.CosmosDB;
using Estimatz.API.Entities.Room;
using Estimatz.API.Entities.UserStory;
using Microsoft.Azure.Cosmos;

namespace Estimatz.API.Data.RoomRepository
{
    public class RoomRepository : IRoomRepository
    {
        private readonly ICosmosDBClient _cosmosDbClient;
        private readonly string _partitioKey = "room";

        public RoomRepository(ICosmosDBClient cosmosDBClient)
        {
            _cosmosDbClient = cosmosDBClient;
        }

        public async Task<ItemResponse<Room>> CreateRoom(Room room)
        {
            room.PartitionKey = _partitioKey;
            return await _cosmosDbClient.CreateItemAsync(room);
        }

        public async Task<Room> FindRoom(Guid id)
        {
            return await _cosmosDbClient.GetItem<Room>(id.ToString(), _partitioKey);
        }

        public List<SimpleRoom> GetAllRoomByUserId(Guid userId)
        {            
            return _cosmosDbClient.GetItemQueryable<Room>().Where(x => x.UserId == userId).Select(x => new SimpleRoom 
            { 
                Id = x.Id,
                PartitionKey = x.PartitionKey,
                RoomName = x.RoomConfig.RoomName,
                Status = x.Status,
                FinishedStories = x.UserStories.Where(y => y.Status == StoryStatus.Finished).Count(),
                TotalCountStories = x.UserStories.Count()
            }).ToList(); 
        }

        public async Task<ItemResponse<Room>> DeleteRoom(Guid id)
        {
            return await _cosmosDbClient.DeleteItem<Room>(id.ToString(), _partitioKey);
        }

        public async Task<ItemResponse<Room>> UpdateStatusRoom(Guid roomId, RoomStatus newRoomStatus)
        {
            List<PatchOperation> operations = new()
            {
                PatchOperation.Set("/status", (int)newRoomStatus)
            };

            return await _cosmosDbClient.PatchUpdate<Room>(roomId.ToString(), _partitioKey, operations);
        }
    }
}
