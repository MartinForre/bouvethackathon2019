using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            return Ok(id);
        }


    }
}
