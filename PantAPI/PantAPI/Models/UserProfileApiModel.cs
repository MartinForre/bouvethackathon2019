using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PantAPI.Models
{
    public class UserProfileApiModel
    {
        public UserProfileApiModel(UserProfile userProfile)
        {
            UserId = userProfile.UserId;
            Name = userProfile.Name;
            Email = userProfile.Email;
            Token = userProfile.Token;
        }

        public string UserId { get; }
        public string Name { get; }
        public string Email { get; }
        public string Token { get; }
    }
}
