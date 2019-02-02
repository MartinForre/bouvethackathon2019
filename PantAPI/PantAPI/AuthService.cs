using Microsoft.AspNetCore.Http;
using PantAPI.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PantAPI
{
    public class AuthService
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly UserRepository userRepository;

        public AuthService(IHttpContextAccessor httpContextAccessor,  UserRepository userRepository)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.userRepository = userRepository;
        }
        public async Task EnsureToken()
        {
            var token = httpContextAccessor.HttpContext.Request.Headers["Authorization"];
            var newToken = await userRepository.AuthenticateAsync(token);

            if(string.IsNullOrEmpty(newToken))
            {
                throw new UnauthorizedAccessException();
            }

            httpContextAccessor.HttpContext.Response.Headers.Add("x-plukk-token", newToken);
        }

        internal async Task AnonymousUser(string userId)
        {
            var newUser = await userRepository.RegisterUser(userId, null, null, null);
            httpContextAccessor.HttpContext.Request.Headers.Add("Authorization", newUser.Token);
        }
    }
}
