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
        private readonly TokenRepository tokenRepository;

        public UserRepository(string connectionString, TokenRepository tokenRepository) : base(TABLE_NAME, connectionString)
        {
            this.tokenRepository = tokenRepository;
        }

        public async Task<UserProfile> RegisterUser(string userId, string email, string name, string password)
        {
            if(string.IsNullOrEmpty(userId))
            {
                userId = Guid.NewGuid().ToString();
            }

            var existingEmail = await GetWhereAsync("Users", query => query.Where(TableQuery.GenerateFilterCondition("Email", QueryComparisons.Equal, email)));
            if(existingEmail.Any())
            {
                throw new InvalidOperationException($"Email [{email}] already exist");
            }

            var user = new UserProfile(userId)
            {
                Email = email,
                Name = name
            };

            if (!string.IsNullOrEmpty(password))
            {
                CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
            }

            var token = tokenRepository.GenerateNewToken();
            await tokenRepository.AddOrUpdateAsync(new UserToken("Tokens", token, user.UserId));
            var addedUser = await AddOrUpdateAsync(user);

            return addedUser;
        }

        public async Task<UserToken> GetTokenForUserAsync(string userId)
        {
            return await tokenRepository.GetTokenForUserAsync(userId);
        }

        public async Task<UserToken> AuthenticateAsync(string token)
        {
            var userToken = await tokenRepository.GetAsync("Tokens", token);
            
            if(userToken == null)
            {
                return null;
            }

            var newToken = tokenRepository.GenerateNewToken();
            await tokenRepository.DeleteAsync(userToken);
            return await tokenRepository.AddOrUpdateAsync(new UserToken("Tokens", token, userToken.UserId));
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
            var passwordHash = user.PasswordHash;
            var passwordSalt = user.PasswordSalt;
            if (!VerifyPasswordHash(password, passwordHash, passwordSalt))
                return null;
            
            var oldToken = await tokenRepository.GetTokenForUserAsync(user.UserId);
            if (oldToken != null)
            {
                await tokenRepository.DeleteAsync(oldToken);
            }

            var token = tokenRepository.GenerateNewToken();
            await tokenRepository.AddOrUpdateAsync(new UserToken("Tokens", token, user.UserId));
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
