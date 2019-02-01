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
        public ActionResult Add()
        {
            var url = "http://bouvet.no/" + Guid.NewGuid().ToString();
            //db-add.
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
