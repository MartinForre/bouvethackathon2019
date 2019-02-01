using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PantAPI.Controllers
{
    [Route("api")]
    public class HelloWorldController : Controller
    {
        [HttpGet]
        [Route("HelloWorld")]
        public string HelloWorld()
        {
            return "HELLO PLASTIC WORLD";
        }
    }
}
