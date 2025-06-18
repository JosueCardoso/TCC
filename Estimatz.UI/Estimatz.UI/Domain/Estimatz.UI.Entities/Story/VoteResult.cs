using Estimatz.UI.Entities.Room;

namespace Estimatz.UI.Entities.Story
{
	public class VoteResult
	{
        public string Average { get; set; } = String.Empty;
        public Dictionary<string, int> Votes { get; set; } = new Dictionary<string, int>();
        public Decks Deck { get; set; }
        public EstimateType EstimateType { get; set; }
    }
}
