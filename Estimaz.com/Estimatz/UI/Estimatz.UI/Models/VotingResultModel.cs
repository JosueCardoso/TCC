using Estimatz.Entities.Room;

namespace Estimatz.UI.Models
{
    public class VotingResultModel
    {
        public string Average { get; set; } = string.Empty;
        public Dictionary<string, int> Votes { get; set; } = new Dictionary<string, int>();
        public Decks Deck { get; set; }
        public EstimateType EstimateType { get; set; }
    }
}
