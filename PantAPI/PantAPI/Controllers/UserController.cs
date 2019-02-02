using Microsoft.AspNetCore.Mvc;
using PantAPI.Models;
using PantAPI.Repositories;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PantAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController: ControllerBase
    {
        private readonly UserRepository userRepository;
        private readonly BagRepository bagRepository;
        private readonly AuthService authService;

        public UserController(UserRepository userRepository, BagRepository bagRepository, AuthService authService)
        {
            this.userRepository = userRepository;
            this.bagRepository = bagRepository;
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

            return Ok(new UserProfileApiModel(user, token?.Token));
        }

        [HttpPost]
        [Route("Register")]
        [ProducesResponseType(typeof(string), 200)]
        public async Task<ActionResult> Register([FromBody] RegisterModel model)
        {
            var registeredUser = await userRepository.RegisterUser(model.userId, model.email, model.name, model.password);
            var token = await userRepository.GetTokenForUserAsync(registeredUser.UserId);
            return Ok(new UserProfileApiModel(registeredUser, token?.Token));
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
            return Ok(new UserProfileApiModel(updatedUser, token?.Token));
        }

        [HttpGet]
        [Route("Balance")]
        [ProducesResponseType(typeof(BalanceResultModel), 200)]
        public async Task<ActionResult> Balance()
        {
            var user = await authService.EnsureAndGetUserAsync();
            
            var bags = await bagRepository.GetBagsForUserAsync(user.UserId);

            return Ok(new BalanceResultModel
            {

                Details = bags.Select(bag => new BagInfo
                {
                    Received = bag.ReceiveDate,
                    Value = bag.Value,
                    Weight = bag.Weight,

                }).ToList(),
                Balance = bags.Where(c => c.Value != null).Sum(c => c.Value.Value)
            });
        }
    }
}
