using Estimatz.API.Entities.UserStory;
using MediatR;

namespace Estimatz.API.Commands.AddStory
{
    public class AddStoryCommand : IRequest
    {
        public Guid RoomId { get; set; }
        public Story Story { get; set; }
    }
}
