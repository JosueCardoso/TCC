using Estimatz.API.Entities.Room;
using Estimatz.API.Entities.UserStory;

namespace Estimatz.API.Services.Calculator
{
    public interface ICalculator
    {
        VotingResult Calcule(List<string> votes, Decks deck);
    }
}
