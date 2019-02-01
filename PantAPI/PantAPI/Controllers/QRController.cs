using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PantAPI.Models;

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
        public ActionResult Add()
        {
            var bagid = Guid.NewGuid().ToString();
            var url = "https://bouvet-panther.azurewebsites.net/activate/" +bagid ;

            var bag = new Bag
            {
                BagCreatedDate = DateTime.UtcNow,
                BagId = bagid,
                Status = BagStatus.Created
            };

            return Ok(url);
        }

        [HttpPost]
        [Route("Activate")]
        [ProducesResponseType(typeof(bool), 200)]
        public ActionResult Activate(string qrCode, string userid)
        {

            //oppdater qr-code-"record" med bruker som har registrert posen
            //sett status som "active?"
            return Ok(true);
            //hvis koden ikke finnes i databasen, returneres false;

        }

        [HttpGet]
        [Route("Generate")]
        [ProducesResponseType(typeof(string), 200)]
        public ActionResult Generate(string qrCode)
        {
            var generator = new QRCoder.QRCodeGenerator();
            var qRCodeData = generator.CreateQrCode(qrCode, QRCoder.QRCodeGenerator.ECCLevel.Q);
            var qRCoder = new QRCoder.SvgQRCode(qRCodeData);
            var resultat = qRCoder.GetGraphic(5);
            return Ok(resultat);
        }


    }
}
