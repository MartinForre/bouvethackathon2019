using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using PantAPI.Models;
using System.Threading.Tasks;

namespace PantAPI.Repositories
{
    public class BagRepository : BaseRepository<Bag>
    {
        const string TABLE_NAME = "bags";
        const string NOT_ASSIGNED_PARTITION_KEY = "NA";

        public BagRepository(string connectionString) : base(TABLE_NAME, connectionString)
        {
        }

        public async Task<Bag> AddNewAsync(string bagId)
        {
            var bag = new Bag(NOT_ASSIGNED_PARTITION_KEY, bagId);
            var insertOperation = TableOperation.Insert(bag);
            var table = await GetTableAsync();
            var res = await table.ExecuteAsync(insertOperation);

            return bag;
        }

        public async Task<Bag> GetUnusedAsync(string bagId)
        {
            return await GetAsync(NOT_ASSIGNED_PARTITION_KEY, bagId);
        }


        public async Task<List<Bag>> GetBagsForUserAsync(string userId)
        {
            var query = new TableQuery<Bag>().Where(TableQuery.GenerateFilterCondition(nameof(Bag.PartitionKey), QueryComparisons.Equal, userId));
            var table = await GetTableAsync();

            List<Bag> results = new List<Bag>();
            TableContinuationToken continuationToken = null;
            do
            {
                TableQuerySegment<Bag> queryResults =
                    await table.ExecuteQuerySegmentedAsync(query, continuationToken);

                continuationToken = queryResults.ContinuationToken;
                results.AddRange(queryResults.Results);

            } while (continuationToken != null);

            return results;
        }

        public async Task<Bag> GetBag(string bagId)
        {
            var query = new TableQuery<Bag>().Where(TableQuery.GenerateFilterCondition(nameof(Bag.RowKey), QueryComparisons.Equal, bagId));
            var table = await GetTableAsync();

            List<Bag> results = new List<Bag>();
            TableContinuationToken continuationToken = null;
            do
            {
                TableQuerySegment<Bag> queryResults =
                    await table.ExecuteQuerySegmentedAsync(query, continuationToken);

                continuationToken = queryResults.ContinuationToken;
                results.AddRange(queryResults.Results);

            } while (continuationToken != null);

            return results.FirstOrDefault();
        }
    }
}