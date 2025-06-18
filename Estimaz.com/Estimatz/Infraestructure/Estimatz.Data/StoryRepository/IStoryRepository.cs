using Estimatz.Entities.Room;
using Estimatz.Entities.UserStory;
using Microsoft.Azure.Cosmos;

namespace Estimatz.Data.StoryRepository
{
    public interface IStoryRepository
    {
        Task<ItemResponse<Room>> AddStory(Guid roomId, UserStory story);
        Task<ItemResponse<Room>> RemoveStory(int indexArray, Guid roomId);
        Task<ItemResponse<Room>> UpdateStatusStory(int indexArray, StoryStatus newStatusStory, Guid roomId);
        Task<ItemResponse<Room>> UpdateStoryVote(Guid roomId, int storyIndex, VotingResult votingResult);
    }
}
