using Estimatz.CosmosDB.CosmosDB;
using Estimatz.Entities.Room;
using Estimatz.Entities.UserStory;
using Microsoft.Azure.Cosmos;

namespace Estimatz.Data.StoryRepository
{
    public class StoryRepository : IStoryRepository
    {
        private readonly ICosmosDBClient _cosmosDbClient;
        private readonly string _partitioKey = "room";

        public StoryRepository(ICosmosDBClient cosmosDbClient)
        {
            _cosmosDbClient = cosmosDbClient;
        }

        public async Task<ItemResponse<Room>> AddStory(Guid roomId, UserStory story)
        {
            List<PatchOperation> operations = new()
            {
                PatchOperation.Add("/userStories/-", story)
            };

            return await _cosmosDbClient.PatchUpdate<Room>(roomId.ToString(), _partitioKey, operations);
        }

        public async Task<ItemResponse<Room>> RemoveStory(int indexArray, Guid roomId)
        {
            List<PatchOperation> operations = new()
            {
                PatchOperation.Remove("/userStories/" + indexArray)
            };

            return await _cosmosDbClient.PatchUpdate<Room>(roomId.ToString(), _partitioKey, operations);
        }

        public async Task<ItemResponse<Room>> UpdateStatusStory(int indexArray, StoryStatus newStatusStory, Guid roomId)
        {
            List<PatchOperation> operations = new()
            {
                PatchOperation.Set("/userStories/" + indexArray + "/status", (int)newStatusStory)
            };

            return await _cosmosDbClient.PatchUpdate<Room>(roomId.ToString(), _partitioKey, operations);
        }

        public async Task<ItemResponse<Room>> UpdateStoryVote(Guid roomId, int storyIndex, VotingResult votingResult)
        {
            List<PatchOperation> operations = new()
            {
                PatchOperation.Set("/userStories/" + storyIndex + "/voteResult", votingResult)
            };

            return await _cosmosDbClient.PatchUpdate<Room>(roomId.ToString(), _partitioKey, operations);
        }
    }
}
