using Estimatz.Entities.UserStory;
using MediatR;

namespace Estimatz.Commands.Story.UpdateStatusStory
{
    public class UpdateStatusStoryCommand : IRequest
    {
        public StoryStatus NewStoryStatus { get; set; }
        public Guid RoomId { get; set; }
        public Guid StoryId { get; set; }
    }
}
