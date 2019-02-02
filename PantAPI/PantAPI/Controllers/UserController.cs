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
        private BagRepository bagRepository;

        public UserController(UserRepository userRepository, BagRepository bagRepository)
        {
            this.userRepository = userRepository;
            this.bagRepository = bagRepository;
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

        [HttpGet]
        [Route("Balance")]
        [ProducesResponseType(typeof(BalanceResultModel), 200)]
        public async Task<ActionResult> Balance(string userId)
        {

            var bags = await bagRepository.GetBagsForUserAsync(userId);

            return Ok(new BalanceResultModel
            {

                Details = bags.Select(bag => new BagInfo
                {
                    Received = bag.ReceiveDate,
                    Value = bag.Value,
                    Weight = bag.Weight,

                }).ToList(),
                Balance = bags.Sum(c => c.Value)
            });
        }
    }
}
