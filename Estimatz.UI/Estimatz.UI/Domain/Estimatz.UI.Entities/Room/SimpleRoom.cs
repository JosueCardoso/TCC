namespace Estimatz.UI.Entities.Room
{
	public class SimpleRoom
	{
        public Guid Id { get; set; }
        public string RoomName { get; set; }
        public RoomStatus Status { get; set; }
        public int FinishedStories { get; set; }
        public int TotalCountStories { get; set; }
    }
}
