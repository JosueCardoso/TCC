using Newtonsoft.Json;

namespace Estimatz.Entities.CosmosDB
{
    public abstract class Document
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("partitionKey")]
        public string PartitionKey { get; set; }
    }
}
