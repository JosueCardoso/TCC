using Microsoft.Azure.Cosmos;

namespace Estimatz.API.CosmosDB.CosmosDB
{
    public interface ICosmosDBClient
    {
        Task<ItemResponse<T>> CreateItemAsync<T>(T item);
        IOrderedQueryable<T> GetItemQueryable<T>();
        Task<ItemResponse<T>> DeleteItem<T>(string id, string partitionKeyString);
        Task<ItemResponse<T>> GetItem<T>(string id, string partitionKeyString);
        Task<ItemResponse<T>> PatchUpdate<T>(string id, string partitionKeyString, List<PatchOperation> patchOperations);
    }
}
