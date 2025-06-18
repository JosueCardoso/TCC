using Estimatz.Entities.UserStory;
using MediatR;

namespace Estimatz.Commands.Story.AddStory
{
    public class AddStoryCommand : IRequest
    {
        public Guid RoomId { get; set; }
        public UserStory Story { get; set; }
    }
}
