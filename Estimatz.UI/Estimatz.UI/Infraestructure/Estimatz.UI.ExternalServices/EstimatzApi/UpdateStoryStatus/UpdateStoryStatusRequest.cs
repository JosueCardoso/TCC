using Estimatz.UI.Entities.Story;

namespace Estimatz.UI.ExternalServices.EstimatzApi.UpdateStoryStatus
{
    public class UpdateStoryStatusRequest
    {
        public StoryStatus NewStoryStatus { get; set; }
        public Guid RoomId { get; set; }
        public Guid StoryId { get; set; }
    }
}
