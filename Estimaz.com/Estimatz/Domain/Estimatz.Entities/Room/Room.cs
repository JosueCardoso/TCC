using Estimatz.Entities.CosmosDB;
using Estimatz.Entities.UserStory;
using Newtonsoft.Json;

namespace Estimatz.Entities.Room
{
	public class Room : Document
    {
        [JsonProperty("status")]
        public RoomStatus Status { get; set; }

        [JsonProperty("userStories")]
        public List<UserStory.UserStory> UserStories { get; set; }

        [JsonProperty("userId")]
        public Guid UserId { get; set; }

        [JsonProperty("roomConfig")]
        public RoomConfig RoomConfig { get; set; }
    }
}
