using Estimatz.UI.Entities.Room;
using Estimatz.UI.Entities.Story;
using MediatR;

namespace Estimatz.UI.Commands.UpdateStatusStory
{
    public class UpdateStatusStoryCommand : IRequest<bool>
    {
        public StoryStatus NewStoryStatus { get; set; }
        public Guid RoomId { get; set; }
        public Guid StoryId { get; set; }
    }
}
