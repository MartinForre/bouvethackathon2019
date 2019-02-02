using Microsoft.AspNetCore.Mvc;
using PantAPI.Models;
using System;

namespace PantAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController: ControllerBase
    {
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
        public ActionResult Balance(string userId)
        {
            return Ok(new BalanceResultModel
            {

                Balance = 0
            });
        }
    }
}
