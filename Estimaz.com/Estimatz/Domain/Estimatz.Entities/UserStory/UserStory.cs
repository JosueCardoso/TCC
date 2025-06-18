using Newtonsoft.Json;

namespace Estimatz.Entities.UserStory
{
    public class UserStory
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("status")]
        public StoryStatus Status { get; set; }

        [JsonProperty("voteResult")]
        public VotingResult VoteResult { get; set; }
    }
}
