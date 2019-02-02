using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PantAPI.Repositories
{
    public abstract class BaseRepository<T> where T : TableEntity, new()
    {
        private readonly string connectionString;
        private readonly string tableName;

        public BaseRepository(string tableName, string connectionString)
        {
            this.tableName = tableName;
            this.connectionString = connectionString;
        }

        protected async Task<CloudTable> GetTableAsync()
        {
            var client = GetCloudTableClient();
            var table = client.GetTableReference(tableName);
            await table.CreateIfNotExistsAsync();
            return table;
        }

        protected CloudTableClient GetCloudTableClient()
        {
            var storageAccount = GetStorageAccount();
            return storageAccount.CreateCloudTableClient();
        }

        protected CloudStorageAccount GetStorageAccount()
        {
            return CloudStorageAccount.Parse(connectionString);
        }

        public async Task<T> AddOrUpdateAsync(T item)
        {
            var insertOperation = TableOperation.InsertOrReplace(item);
            var table = await GetTableAsync();
            await table.ExecuteAsync(insertOperation);

            return item;
        }

        public async Task<T> GetAsync(string partitionKey, string rowKey)
        {
            var retrieveOperation = TableOperation.Retrieve<T>(partitionKey, rowKey);
            var table = await GetTableAsync();
            var result = await table.ExecuteAsync(retrieveOperation);

            return (T)result.Result;
        }

        public async Task<IEnumerable<T>> GetWhereAsync(string partitionKey, Func<TableQuery<T>, TableQuery<T>> where)
        {
            var query = where(new TableQuery<T>());
            var table = await GetTableAsync();

            List<T> results = new List<T>();
            TableContinuationToken continuationToken = null;
            do
            {
                TableQuerySegment<T> queryResults = await table.ExecuteQuerySegmentedAsync(query, continuationToken);

                continuationToken = queryResults.ContinuationToken;
                results.AddRange(queryResults.Results);

            } while (continuationToken != null);

            return results;
        }

        public async Task DeleteAsync(T item)
        {
            var deleteOperation = TableOperation.Delete(item);
            var table = await GetTableAsync();
            await table.ExecuteAsync(deleteOperation);
        }
    }
}
