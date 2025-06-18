using MediatR;

namespace Estimatz.API.Commands.RemoveStory
{
    public class RemoveStoryCommand : IRequest
    {
        public Guid StoryId { get; set; }
        public Guid RoomId { get; set; }
    }
}
