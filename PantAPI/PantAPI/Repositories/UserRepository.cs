using Microsoft.WindowsAzure.Storage.Table;
using PantAPI.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PantAPI.Repositories
{
    public class UserRepository : BaseRepository<UserProfile>
    {
        const string TABLE_NAME = "users";

        public UserRepository(string connectionString) : base(TABLE_NAME, connectionString)
        {
        }

        public async Task<UserProfile> RegisterUser(string userId, string email, string name, string password)
        {
            if(string.IsNullOrEmpty(userId))
            {
                userId = Guid.NewGuid().ToString();
            }

            var user = new UserProfile(userId)
            {
                Email = email,
                Token = GetNewToken(),
                Name = name
            };

            if (!string.IsNullOrEmpty(password))
            {
                CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

                user.PasswordHash = System.Text.Encoding.Default.GetString(passwordHash);
                user.PasswordSalt = System.Text.Encoding.Default.GetString(passwordSalt);
            }

            var addedUser = await AddOrUpdateAsync(user);

            return addedUser;
        }

        private string GetNewToken()
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(Guid.NewGuid().ToString());
            return System.Convert.ToBase64String(plainTextBytes);
        }

        public async Task<string> AuthenticateAsync(string token)
        {
            var users = await GetWhereAsync("Users", query =>
                query.Where(TableQuery.GenerateFilterCondition("Token", QueryComparisons.Equal, token)));

            var user = users.ToList().SingleOrDefault();

            if(user == null)
            {
                return null;
            }

            user.Token = GetNewToken();
            await AddOrUpdateAsync(user);

            return user.Token;
        }

        public async Task<UserProfile> AuthenticateAsync(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                return null;

            var users = await GetWhereAsync("Users", query => 
            query.Where(TableQuery.GenerateFilterCondition("Email", QueryComparisons.Equal, email)));

            var user = users.ToList().SingleOrDefault();

            // check if user exists
            if (user == null)
                return null;

            // check if password is correct
            var passwordHash = System.Text.Encoding.UTF8.GetBytes(user.PasswordHash);
            var passwordSalt = System.Text.Encoding.UTF8.GetBytes(user.PasswordSalt);
            if (!VerifyPasswordHash(password, passwordHash, passwordSalt))
                return null;

            user.Token = GetNewToken();
            await AddOrUpdateAsync(user);

            return user;
        }

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }
    }
}
