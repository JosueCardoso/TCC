using Estimatz.Entities.UserStory;
using MediatR;

namespace Estimatz.Commands.Story.UpdateStoryVote
{
    public class UpdateStoryVoteCommand : IRequest
    {
        public VotingResult VotingResult { get; set; }
        public Guid RoomId { get; set; }
        public Guid StoryId { get; set; }
    }
}
