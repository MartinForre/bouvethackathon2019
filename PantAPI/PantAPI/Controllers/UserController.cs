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

        public UserController(UserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult> Login([FromBody] LoginModel model)
        {
            var user = await userRepository.AuthenticateAsync(model.Username, model.Password);

            if(user != null)
            {
                return Unauthorized();
            }

            return Ok(new UserProfileApiModel(user));
        }

        [HttpPost]
        [Route("Register")]
        [ProducesResponseType(typeof(string), 200)]
        public async Task<ActionResult> Register([FromBody] RegisterModel model)
        {
            var registeredUser = await userRepository.RegisterUser(model.userId, model.email, model.name, model.password);
            return Ok(new UserProfileApiModel(registeredUser));
        }
    }
}
