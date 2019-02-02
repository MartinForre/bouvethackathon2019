using Microsoft.AspNetCore.Http;
using PantAPI.Models;
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
        private readonly TokenRepository tokenRepository;

        public AuthService(IHttpContextAccessor httpContextAccessor,  UserRepository userRepository, TokenRepository tokenRepository)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.userRepository = userRepository;
            this.tokenRepository = tokenRepository;
        }
        public async Task EnsureTokenAsync()
        {
            var token = httpContextAccessor.HttpContext.Request.Headers["Authorization"];
            var newToken = await userRepository.AuthenticateAsync(token);

            if(newToken == null)
            {
                throw new UnauthorizedAccessException();
            }

            httpContextAccessor.HttpContext.Response.Headers.Add("x-plukk-token", newToken.Token);
        }

        public async Task<UserProfile> EnsureAndGetUserAsync()
        {
            var token = httpContextAccessor.HttpContext.Request.Headers["Authorization"];
            var newToken = await userRepository.AuthenticateAsync(token);

            if (newToken == null)
            {
                throw new UnauthorizedAccessException();
            }

            httpContextAccessor.HttpContext.Response.Headers.Add("x-plukk-token", newToken.Token);

            return await userRepository.GetAsync("Users", newToken.UserId);
        }

        internal async Task AnonymousUser(string userId)
        {
            var newToken = tokenRepository.GenerateNewToken();
            await tokenRepository.AddOrUpdateAsync(new UserToken("Tokens", newToken, userId));
            await userRepository.AddOrUpdateAsync(new UserProfile(userId));

            if (httpContextAccessor.HttpContext.Request.Headers.ContainsKey("Authorization"))
            {
                httpContextAccessor.HttpContext.Request.Headers["Authorization"] = newToken;
            }
            else
            {
                httpContextAccessor.HttpContext.Request.Headers.Add("Authorization", newToken);
            }
        }
    }
}
