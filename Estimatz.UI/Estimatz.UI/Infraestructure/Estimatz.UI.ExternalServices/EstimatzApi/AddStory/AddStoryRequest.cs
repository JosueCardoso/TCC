using Estimatz.UI.Entities.Story;

namespace Estimatz.UI.ExternalServices.EstimatzApi.AddStory
{
    public class AddStoryRequest
    {
        public Guid RoomId { get; set; }
        public UserStory Story { get; set; }
    }
}
