using Estimatz.Entities.AppConfig;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Options;

namespace Estimatz.CosmosDB.CosmosDB
{
    public class CosmosDBClient : ICosmosDBClient
    {
        private readonly string _endpointUri;
        private readonly string _primaryKey;
        private CosmosClient _cosmosClient;
        private Database _dataBase;
        private Container _container;

        public CosmosDBClient(IOptions<CosmosConfig> configuration)
        {
            _endpointUri = configuration.Value.Url;
            _primaryKey = configuration.Value.PrimaryKey;

            if (_cosmosClient == null)
            {
                var cosmosClientOptions = new CosmosClientOptions
                {
                    ConnectionMode = ConnectionMode.Gateway,
                    AllowBulkExecution = true
                };

                _cosmosClient = new CosmosClient(_endpointUri, _primaryKey, cosmosClientOptions);

                if(_cosmosClient is not null)
                {
                    _dataBase = _cosmosClient.GetDatabase(configuration.Value.Database);

                    if(_dataBase is not null)
                    {
                        _container = _dataBase.GetContainer(configuration.Value.Container);
                    }
                }
            }
        }

        public async Task<ItemResponse<T>> CreateItemAsync<T>(T item)
        {
            return await _container.CreateItemAsync(item);
        }

        public IOrderedQueryable<T> GetItemQueryable<T>()
        {
            return _container.GetItemLinqQueryable<T>(allowSynchronousQueryExecution: true);
        }

        public async Task<ItemResponse<T>> DeleteItem<T>(string id, string partitionKeyString)
        {
            PartitionKey partitionKey = new PartitionKey(partitionKeyString);
            return await _container.DeleteItemAsync<T>(id, partitionKey);
        }

        public async Task<ItemResponse<T>> GetItem<T>(string id, string partitionKeyString)
        {
            PartitionKey partitionKey = new PartitionKey(partitionKeyString);
            return await _container.ReadItemAsync<T>(id, partitionKey);
        }

        public async Task<ItemResponse<T>> PatchUpdate<T>(string id, string partitionKeyString, List<PatchOperation> patchOperations)
        {
            PartitionKey partitionKey = new PartitionKey(partitionKeyString);
            return await _container.PatchItemAsync<T>(id, partitionKey, patchOperations);
        }
    }
}
