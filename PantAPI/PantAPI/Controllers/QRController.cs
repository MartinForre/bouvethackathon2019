using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PantAPI.Models;
using PantAPI.Repositories;

namespace PantAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QRController : ControllerBase
    {
        private BagRepository bagRepository;

        public QRController(BagRepository bagRepository)
        {
            this.bagRepository = bagRepository;
        }


        [HttpGet]
        [Route("tull")]
        public async Task<Bag> Tull([FromServices] BagRepository bagRepository)
        {
            var bagId = "d9c47d23-f35d-41e4-9652-99bd8609534d";
            var userId = "94d0266d-b4c0-47f2-88fd-d25ec8829915";
            
            return await bagRepository.GetAsync(userId, bagId);
        }

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
                CreatedDate = DateTime.UtcNow,
                Status = BagStatus.Created
            };

            return Ok(url);
        }

        [HttpPost]
        [Route("Activate")]
        [ProducesResponseType(typeof(ActivateResultModel), 200)]
        public async Task<ActionResult> Activate([FromBody] ActivateModel activateModel)
        {
            var bag = await bagRepository.GetAsync("NA", activateModel.BagId);
            if (bag == null)
            {
                return Ok(new ActivateResultModel
                {
                    Status = ActivativateStatus.Unknown
                });
            }

            if (string.IsNullOrEmpty(activateModel.UserId))
            {
                activateModel.UserId = Guid.NewGuid().ToString();
            }

            await bagRepository.DeleteAsync(bag);

            bag.PartitionKey = activateModel.UserId;
            await bagRepository.AddOrUpdateAsync(bag);
            
            return Ok(new ActivateResultModel
            {
                Status = ActivativateStatus.OK,
                UserId = bag.UserId,
                BagId = bag.BagId
            });
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
