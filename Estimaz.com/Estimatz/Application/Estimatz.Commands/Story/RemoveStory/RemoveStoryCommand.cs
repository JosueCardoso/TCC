using MediatR;

namespace Estimatz.Commands.Story.RemoveStory
{
    public class RemoveStoryCommand : IRequest
    {
        public Guid StoryId { get; set; }
        public Guid RoomId { get; set; }
    }
}
