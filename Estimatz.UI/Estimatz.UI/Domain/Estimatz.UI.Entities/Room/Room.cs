using Estimatz.UI.Entities.Story;

namespace Estimatz.UI.Entities.Room
{
	public class Room
	{
		public Guid Id { get; set; }
        public RoomStatus Status { get; set; }
        public List<UserStory> UserStories { get; set; }
        public Guid UserId { get; set; }
        public RoomConfig RoomConfig { get; set; }
    }
}
