using Newtonsoft.Json;

namespace Estimatz.API.Entities.UserStory
{
    public class Story
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
