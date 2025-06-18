using Estimatz.UI.Entities.Room;
using Estimatz.UI.ExternalServices.EstimatzApi.AddStory;
using Estimatz.UI.ExternalServices.EstimatzApi.GetAllSimpleRooms;
using Estimatz.UI.ExternalServices.EstimatzApi.GetIndicators;
using Estimatz.UI.ExternalServices.EstimatzApi.GetRoom;
using Estimatz.UI.ExternalServices.EstimatzApi.GetStory;
using Estimatz.UI.ExternalServices.EstimatzApi.RemoveStory;
using Estimatz.UI.ExternalServices.EstimatzApi.UpdateStoryStatus;
using Estimatz.UI.ExternalServices.EstimatzLoginApi.Common;

namespace Estimatz.UI.ExternalServices.EstimatzApi
{
    public interface IEstimatzApiClient
    {
        Task<Guid> CreateRoom(Room room);
        Task<GetAllSimpleRoomsResponse> GetAllSimpleRooms(Guid userId);
        Task<CommonResponse> DeleteRoom(Guid roomId, Guid userId);
        Task<GetRoomResponse> GetRoom(Guid roomId, Guid userId);
        Task<CommonResponse> AddStory(AddStoryRequest request);
        Task<CommonResponse> RemoveStory(RemoveStoryRequest request);
        Task<CommonResponse> UpdateStoryStatus(UpdateStoryStatusRequest request);
        Task<GetStoryResponse> GetStory(Guid roomId, Guid storyId);
        Task<GetIndicatorsResponse> GetIndicators(Guid userId);
    }
}
