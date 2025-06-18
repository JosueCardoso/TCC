using Estimatz.Entities.CosmosDB;

namespace Estimatz.Entities.Room
{
	public class SimpleRoom : Document
    {
        public string RoomName { get; set; }
        public RoomStatus Status { get; set; }
        public int FinishedStories { get; set; }
        public int TotalCountStories { get; set; }
    }
}
