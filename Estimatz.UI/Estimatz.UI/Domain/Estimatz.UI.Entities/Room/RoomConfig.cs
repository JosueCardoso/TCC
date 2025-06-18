namespace Estimatz.UI.Entities.Room
{
    public class RoomConfig
    {
        public List<Team> Teams { get; set; }
        public EstimateType EstimateType { get; set; }
        public bool DivideTeams { get; set; }
        public bool IntersperseTeams { get; set; }
        public VotingType VotingType { get; set; }
        public Decks Deck { get; set; }
        public string RoomName { get; set; }
        public bool IsQuickRoom { get; set; }
    }
}
