using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using PantAPI.Models;

namespace PantAPI.Repositories
{
    public class BagRepository
    {
        const string TABLE_NAME = "bags";
        private readonly string connectionString;

        public BagRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public async Task<Bag> AddOrUpdateAsync(Bag bag)
        {
            var insertOperation = TableOperation.InsertOrReplace(bag);
            var table = await GetTableAsync();
            await table.ExecuteAsync(insertOperation);

            return bag;
        }

        public async Task<Bag> GetAsync(string userId, string bagId)
        {
            var retrieveOperation = TableOperation.Retrieve<Bag>(userId, bagId);
            var table = await GetTableAsync();
            var result = await table.ExecuteAsync(retrieveOperation);

            return (Bag)result.Result;
        }

        public async Task DeleteAsync(Bag bag)
        {
            var deleteOperation = TableOperation.Delete(bag);
            var table = await GetTableAsync();
            await table.ExecuteAsync(deleteOperation);
        }

        private async Task<CloudTable> GetTableAsync()
        {
            var client = GetCloudTableClient();
            var table = client.GetTableReference(TABLE_NAME);
            await table.CreateIfNotExistsAsync();
            return table;
        }

        private CloudTableClient GetCloudTableClient()
        {
            var storageAccount = GetStorageAccount();
            return storageAccount.CreateCloudTableClient();
        }

        private CloudStorageAccount GetStorageAccount()
        {
            return CloudStorageAccount.Parse(connectionString);
        }
    }
}