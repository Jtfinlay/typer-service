using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TyperService.Dependencies.TableStorage
{
    /// <summary>
    /// Describes a class that performs common table actions for a generic entity.
    /// </summary>
    /// <typeparam name="T">Generic entity to lookup from the store</typeparam>
    public class AzureTableStorage<T>
        where T : TableEntity, new()
    {
        private readonly CloudTable _table;

        /// <summary>
        /// Initialize an instance of the <see cref="AzureTableStorage{T}"/> class.
        /// </summary>
        /// <param name="tableSettings"></param>
        public AzureTableStorage(AzureTableSettings tableSettings)
        {
            var storageAccount = CloudStorageAccount.Parse(tableSettings.ConnectionString);
            var client = storageAccount.CreateCloudTableClient();
            _table = client.GetTableReference(tableSettings.TableName);
        }

        /// <summary>
        /// Retrieve all entities in a partition from the table.
        /// </summary>
        /// <param name="partitionKey">Sets filter for entries in key's partition.</param>
        /// <returns>Asynchronous task returning a list of entries.</returns>
        public async Task<List<T>> GetEntities(string partitionKey)
        {
            TableQuery<T> query = new TableQuery<T>()
                .Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, partitionKey));

            List<T> results = new List<T>();
            TableContinuationToken continuationToken = null;
            do
            {
                TableQuerySegment<T> querySegment = await _table.ExecuteQuerySegmentedAsync<T>(query, continuationToken);
                continuationToken = querySegment.ContinuationToken;
                results.AddRange(querySegment.Results);
            }
            while (continuationToken != null);

            return results;
        }

        /// <summary>
        /// Retrieve a single, specific entity from the table.
        /// </summary>
        /// <param name="partitionKey">Identifies the target partition.</param>
        /// <param name="rowKey">Identifies the target row in the partition.</param>
        /// <returns>Asynchronous task returning an entry on completion, or null.</returns>
        public async Task<T> GetEntity(string partitionKey, string rowKey)
        {
            TableOperation retrieveOperation = TableOperation.Retrieve<T>(partitionKey, rowKey);

            TableResult tableResult = await _table.ExecuteAsync(retrieveOperation);
            return tableResult.Result as T;
        }

        /// <summary>
        /// Insert an entity into the table.
        /// </summary>
        /// <param name="entity">Entity to insert to the store.</param>
        /// <returns>Asynchronous task returning the stored entity on completion.</returns>
        public async Task<T> InsertEntity(T entity)
        {
            TableOperation insertOperation = TableOperation.Insert(entity);

            TableResult tableResult = await _table.ExecuteAsync(insertOperation);
            return tableResult.Result as T;
        }
    }
}
