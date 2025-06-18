using Estimatz.Entities.Room;
using Estimatz.Entities.UserStory;
using MediatR;
using Newtonsoft.Json;

namespace Estimatz.Commands.Room.SaveRoom
{
    public class SaveRoomCommand :  IRequest<Guid>
    {
        [JsonProperty("status")]
        public RoomStatus Status { get; set; }

        [JsonProperty("userStories")]
        public List<UserStory>? UserStories { get; set; }

        [JsonProperty("userId")]
        public Guid UserId { get; set; }

        [JsonProperty("roomConfig")]
        public RoomConfig RoomConfig { get; set; }
    }
}
