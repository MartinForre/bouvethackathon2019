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
    }
}
