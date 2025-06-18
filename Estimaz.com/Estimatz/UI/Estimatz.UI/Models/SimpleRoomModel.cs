using Estimatz.Entities.Room;
using Estimatz.Util.Extensions;

namespace Estimatz.UI.Models
{
    public class SimpleRoomModel
    {
        public Guid Id { get; set; }
        public RoomStatus RoomStatus { get; set; }
        public string RoomName { get; set; }
        public int FinishedStories { get; set; }
        public int TotalStories { get; set; }
        public string RoomStatusDescription => RoomStatus.GetDescription();

        public string GetClassByRoomStatus()
        {
            if (RoomStatus == RoomStatus.NotStarted)
                return "not-started-status";

            if (RoomStatus == RoomStatus.Unfinished)
                return "unfinished-status";

            if (RoomStatus == RoomStatus.Finished)
                return "finished-status";

            return "free-voting-status";
        }
    }
}
