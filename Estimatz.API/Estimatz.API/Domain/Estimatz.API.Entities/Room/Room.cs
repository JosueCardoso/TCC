using Estimatz.API.Entities.CosmosDB;
using Estimatz.API.Entities.UserStory;
using Newtonsoft.Json;

namespace Estimatz.API.Entities.Room
{
    public class Room : Document
    {
        [JsonProperty("status")]
        public RoomStatus Status { get; set; }

        [JsonProperty("userStories")]
        public List<Story> UserStories { get; set; }

        [JsonProperty("userId")]
        public Guid UserId { get; set; }

        [JsonProperty("roomConfig")]
        public RoomConfig RoomConfig { get; set; }
    }
}
