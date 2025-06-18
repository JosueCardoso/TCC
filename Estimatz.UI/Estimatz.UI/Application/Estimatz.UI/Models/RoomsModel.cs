using Estimatz.UI.Entities.Room;
using Estimatz.UI.Entities.Story;
using Estimatz.UI.Extensions;
using Newtonsoft.Json;

namespace Estimatz.UI.Models
{
    public class RoomsModel
    {
        public RoomsModel()
        {
            Rooms = new List<SimpleRoomModel>();
        }

        public List<SimpleRoomModel> Rooms { get; set; }

        public bool HasRooms => Rooms != null && Rooms.Count > 0;
    }

    public class RoomModel
    {
        public RoomModel()
        {
            UserStories = new List<UserStoryModel>();
            RoomConfig = new RoomConfigModel();
        }

        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public RoomStatus RoomStatus { get; set; }
        public List<UserStoryModel> UserStories { get; set; }
        public RoomConfigModel RoomConfig { get; set; }
    }

    public class UserStoryModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public StoryStatus Status { get; set; }
        public string StatusDescription => Status.GetDescription();
        public VotingResultModel VotingResult { get; set; } = new VotingResultModel();
        public bool StorySelected { get; set; }
        public bool UserIsAdmin { get; set; }
    }

    public class RoomConfigModel
    {
        public RoomConfigModel()
        {
            Teams = new List<TeamsModel>();       
        }

        public string RoomName { get; set; }
        public int SelectedDeck { get; set; }
        public Decks Deck { get; set; }
        public int SelectedEstimateType { get; set; }
        public EstimateType EstimateType { get; set; }
        public bool DivideTeams { get; set; }
        public bool IntersperseTeams { get; set; }
        public bool FreeVoting { get; set; }
        public bool TaskVoting { get; set; }
        public bool IsQuickRoom { get; set; }
        public List<TeamsModel> Teams { get; set; }
        public VotingType VotingType => FreeVoting ? VotingType.FreeVoting : VotingType.TaskVoting;

        public List<(int, string)> GetEstimateTypes()
        {
            var estimateTypesDescription = new List<(int, string)>();

            foreach(var estimateType in Enum.GetValues(typeof(EstimateType)))
            {
                var estimateTypeEnum = (EstimateType)estimateType;
                var estimateTypeForList = ((int)estimateTypeEnum, estimateTypeEnum.GetDescription());
                estimateTypesDescription.Add(estimateTypeForList);
            }

            return estimateTypesDescription;
        }

        public List<(int, string)> GetDecks()
        {
            var deckDescription = new List<(int, string)>();

            foreach (var deck in Enum.GetValues(typeof(Decks)))
            {
                var deckEnum = (Decks)deck;
                var deckEnumForList = ((int)deckEnum, deckEnum.GetDescription());
                deckDescription.Add(deckEnumForList);
            }

            return deckDescription;
        }
    }

    public class TeamsModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }

    public class DeckModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
