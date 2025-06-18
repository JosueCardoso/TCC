using Estimatz.Entities.Room;
using Estimatz.Entities.UserStory;

namespace Estimatz.Services.Calculator
{
    public class DefaultCalculator : ICalculator
    {
        public VotingResult Calcule(List<string> votes, Decks deck)
        {
            var result = GetAmountByCard(votes);
            result.Average = GetAverageByDeck(votes, deck);
            result.Deck = deck;
            result.EstimateType = EstimateType.Default;

            return result;
        }

        private VotingResult GetAmountByCard(List<string> votes) 
        {
            var result = new VotingResult();
            votes.ForEach(vote => result.AddNewVote(vote));            

            return result;
        }

        private string GetAverageByDeck(List<string> votes, Decks deck)
        {
            if (deck == Decks.TShirt)
                return string.Empty;

            double amount = 0;
            int count = 0;

            foreach (var vote in votes)
            {
                if (vote == "meio")
                {
                    amount += 0.5;
                    count++;
                }
                else if (int.TryParse(vote, out var result))
                {
                    amount += result;
                    count++;
                }
            }

            if(count == 0)
                return string.Empty;

            return Math.Round(amount/count,2).ToString();
        }
    }
}
