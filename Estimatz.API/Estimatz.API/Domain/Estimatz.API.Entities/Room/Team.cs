using Newtonsoft.Json;

namespace Estimatz.API.Entities.Room
{
    public class Team
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
