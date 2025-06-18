using Estimatz.UI.Entities.Story;
using MediatR;

namespace Estimatz.UI.Queries.GetStory
{
    public class GetStoryQuery : IRequest<UserStory>
    {
        public Guid RoomId { get; set; }
        public Guid StoryId { get; set; }
    }
}
