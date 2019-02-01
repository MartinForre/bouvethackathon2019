using Microsoft.AspNetCore.Mvc;
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
    }
}
