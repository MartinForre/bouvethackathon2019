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
    }
}