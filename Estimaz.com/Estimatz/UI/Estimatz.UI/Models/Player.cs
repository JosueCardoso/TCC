namespace Estimatz.UI.Models
{
    public class PlayerModel
    {
        public string Name { get; set; }
        public Guid UserId { get; set; }
        public List<StoryVote> Votes { get; set; } = new List<StoryVote>();
        public Guid SelectedStory { get; set; }
    }

    public class StoryVote
    {
        public Guid StoryId { get; set; }
        public string Vote { get; set; }
        public bool TurnCards { get; set; }
    }
}
