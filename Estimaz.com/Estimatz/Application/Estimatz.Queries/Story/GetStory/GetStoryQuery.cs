using Estimatz.Entities.UserStory;
using MediatR;

namespace Estimatz.API.Queries.GetStory
{
    public class GetStoryQuery : IRequest<UserStory>
    {
        public Guid RoomId { get; set; }    
        public Guid StoryId { get; set; }
    }
}
