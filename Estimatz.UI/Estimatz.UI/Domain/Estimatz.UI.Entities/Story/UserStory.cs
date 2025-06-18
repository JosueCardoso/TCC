namespace Estimatz.UI.Entities.Story
{
	public class UserStory
	{
        public Guid Id { get; set; }
        public string Name { get; set; }
        public StoryStatus Status { get; set; }
        public VoteResult VoteResult { get; set; } = new VoteResult();
    }
}
