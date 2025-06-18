using Estimatz.Entities.Room;
using Estimatz.Entities.UserStory;

namespace Estimatz.Services.Calculator
{
    public interface ICalculator
    {
        VotingResult Calcule(List<string> votes, Decks deck);
    }
}
