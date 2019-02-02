using Microsoft.WindowsAzure.Storage.Table;

namespace PantAPI.Models
{
    public class UserProfile : TableEntity
    {
        public UserProfile(string userId) : base("Users", userId)
        {
            UserId = userId;
        }

        public UserProfile()
        {
        }

        public string UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public string Token { get; set; }
    }
}
