using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PantAPI.Models
{
    public class UserProfileApiModel
    {
        public UserProfileApiModel(UserProfile userProfile, string token)
        {
            UserId = userProfile.UserId;
            Name = userProfile.Name;
            Email = userProfile.Email;
            Token = token;
        }

        public string UserId { get; }
        public string Name { get; }
        public string Email { get; }
        public string Token { get; }
    }
}
