using PantAPI.Models;

namespace PantAPI.Repositories
{
    public class UserRepository : BaseRepository<UserProfile>
    {
        const string TABLE_NAME = "users";

        public UserRepository(string connectionString) : base(TABLE_NAME, connectionString)
        {
        }
    }
}
