using Estimatz.API.Entities.UserStory;
using MediatR;

namespace Estimatz.API.Queries.GetStory
{
    public class GetStoryQuery : IRequest<Story>
    {
        public Guid RoomId { get; set; }    
        public Guid StoryId { get; set; }
    }
}
