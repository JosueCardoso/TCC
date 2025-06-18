using Estimatz.API.Entities.UserStory;
using MediatR;

namespace Estimatz.API.Commands.UpdateStatusStory
{
    public class UpdateStatusStoryCommand : IRequest
    {
        public StoryStatus NewStoryStatus { get; set; }
        public Guid RoomId { get; set; }
        public Guid StoryId { get; set; }
    }
}
