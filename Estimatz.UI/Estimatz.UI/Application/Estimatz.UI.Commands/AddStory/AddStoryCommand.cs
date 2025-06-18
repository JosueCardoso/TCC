using Estimatz.UI.Entities.Story;
using MediatR;

namespace Estimatz.UI.Commands.AddStory
{
    public class AddStoryCommand : IRequest<bool>
    {
        public Guid RoomId { get; set; }
        public UserStory Story { get; set; }
    }
}
