using MediatR;

namespace Estimatz.UI.Commands.RemoveStory
{
    public class RemoveStoryCommand : IRequest<bool>
    {
        public Guid RoomId { get; set; }
        public Guid StoryId { get; set; }
    }
}
