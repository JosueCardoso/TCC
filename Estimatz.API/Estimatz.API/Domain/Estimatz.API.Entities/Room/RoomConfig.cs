using Newtonsoft.Json;

namespace Estimatz.API.Entities.Room
{
    public class RoomConfig
    {
        [JsonProperty("teams")]
        public List<Team> Teams { get; set; }

        [JsonProperty("estimateType")]
        public EstimateType EstimateType { get; set; }

        [JsonProperty("divideTeams")]
        public bool DivideTeams { get; set; }

        [JsonProperty("intersperseTeams")]
        public bool IntersperseTeams { get; set; }

        [JsonProperty("votingType")]
        public VotingType VotingType { get; set; }

        [JsonProperty("deck")]
        public Decks Deck { get; set; }

        [JsonProperty("roomName")]
        public string RoomName { get; set; }

        [JsonProperty("isQuickRoom")]
        public bool IsQuickRoom { get; set; }
    }
}
