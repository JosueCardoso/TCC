namespace Estimatz.Entities.User
{
    public class UserPlanning
    {
        public string Name { get; set; }
        public Guid UserId { get; set; }
        public Guid RoomId { get; set; }
        public UserRoomRole Role { get; set; }
        public bool IsAdmin { get; set; }        
        public List<StoryVote> Votes { get; set; }
    }

    public class StoryVote
    {
        public Guid StoryId { get; set; }
        public string Vote { get; set; }
        public bool TurnCards { get; set; }
    }
}
