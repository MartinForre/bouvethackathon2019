using Microsoft.AspNetCore.Mvc;
using PantAPI.Models;
using PantAPI.Repositories;
using System;
using System.Threading.Tasks;

namespace PantAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController: ControllerBase
    {
        private readonly UserRepository userRepository;
        private readonly AuthService authService;

        public UserController(UserRepository userRepository, AuthService authService)
        {
            this.userRepository = userRepository;
            this.authService = authService;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult> Login([FromBody] LoginModel model)
        {
            var user = await userRepository.AuthenticateAsync(model.Username, model.Password);

            if(user == null)
            {
                return Unauthorized();
            }

            var token = await userRepository.GetTokenForUserAsync(user.UserId);

            return Ok(new UserProfileApiModel(user, token));
        }

        [HttpPost]
        [Route("Register")]
        [ProducesResponseType(typeof(string), 200)]
        public async Task<ActionResult> Register([FromBody] RegisterModel model)
        {
            var registeredUser = await userRepository.RegisterUser(model.userId, model.email, model.name, model.password);
            var token = await userRepository.GetTokenForUserAsync(registeredUser.UserId);
            return Ok(new UserProfileApiModel(registeredUser, token));
        }

        [HttpPost]
        [Route("Update")]
        [ProducesResponseType(typeof(string), 200)]
        public async Task<ActionResult> Update([FromBody] UpdateModel model)
        {
            await authService.EnsureTokenAsync();
            var user = await userRepository.GetAsync("User", model.UserId);

            if (user != null)
            {
                return Unauthorized();
            }

            var updatedUser = await userRepository.RegisterUser(model.UserId, model.Email, model.Name, model.Password);
            var token = await userRepository.GetTokenForUserAsync(updatedUser.UserId);
            return Ok(new UserProfileApiModel(updatedUser, token));
        }
    }
}
