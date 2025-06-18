using Estimatz.API.Entities.Room;
using Estimatz.API.Entities.UserStory;
using MediatR;
using Newtonsoft.Json;

namespace Estimatz.API.Commands.SaveRoom
{
    public class SaveRoomCommand :  IRequest<Guid>
    {
        [JsonProperty("status")]
        public RoomStatus Status { get; set; }

        [JsonProperty("userStories")]
        public List<Story>? UserStories { get; set; }

        [JsonProperty("userId")]
        public Guid UserId { get; set; }

        [JsonProperty("roomConfig")]
        public RoomConfig RoomConfig { get; set; }
    }
}
