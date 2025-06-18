using Estimatz.Entities.Room;

namespace Estimatz.Entities.UserStory
{
    public class VotingResult
    {
        public string Average { get; set; }
        public Dictionary<string, int> Votes { get; set; } = new Dictionary<string, int>();
        public Decks Deck { get; set; }
        public EstimateType EstimateType { get; set; }

        public void AddNewVote(string vote)
        {
            if (Votes.ContainsKey(vote))            
                Votes[vote]++;            
            else            
                Votes.Add(vote, 1);            
        }
    }
}
