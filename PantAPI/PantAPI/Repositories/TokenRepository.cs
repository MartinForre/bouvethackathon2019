using Microsoft.WindowsAzure.Storage.Table;
using PantAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PantAPI.Repositories
{
    public class TokenRepository : BaseRepository<UserToken>
    {
        public TokenRepository(string connectionString) : base("tokens", connectionString)
        {
        }

        public string GenerateNewToken()
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(Guid.NewGuid().ToString());
            return System.Convert.ToBase64String(plainTextBytes);
        }

        public async Task<UserToken> GetTokenForUserAsync(string userId)
        {
            var tokens = await GetWhereAsync("Tokens", query =>
                query.Where(TableQuery.GenerateFilterCondition("UserId", QueryComparisons.Equal, userId)));

            var token = tokens.SingleOrDefault();

            return token;
        }
    }
}
