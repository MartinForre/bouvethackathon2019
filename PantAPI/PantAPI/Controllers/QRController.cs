using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace PantAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QRController : ControllerBase
    {
        // GET api/values
        [HttpPost]
        [Route("Add")]
        [ProducesResponseType(typeof(string), 200)]
        public ActionResult Add(string qrcode)
        {
            return Ok(qrcode);
        }
      
    }
}
