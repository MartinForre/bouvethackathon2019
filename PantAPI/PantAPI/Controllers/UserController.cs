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
        private BagRepository bagRepository;

        public UserController(BagRepository bagRepository)
        {
            this.bagRepository = bagRepository;
        }


        [HttpGet]
        [Route("Register")]
        [ProducesResponseType(typeof(string), 200)]
        public ActionResult Register()
        {
            string id = Guid.NewGuid().ToString();
            return Ok( new { uid = id });
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
                Balance = 0
            });
        }
    }
}
