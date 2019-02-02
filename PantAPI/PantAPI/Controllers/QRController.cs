using Microsoft.AspNetCore.Mvc;
using PantAPI.Models;
using PantAPI.Repositories;
using System;
using System.Threading.Tasks;

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

        [HttpPost]
        [Route("Activate")]
        [ProducesResponseType(typeof(ActivateResultModel), 200)]
        public async Task<ActionResult> Activate([FromBody] ActivateModel activateModel)
        {
            var bag = await bagRepository.GetUnusedAsync(activateModel.BagId);
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
        public async Task<ActionResult> Generate()
        {
            var bagId = Guid.NewGuid().ToString();
            var baseUrl = "https://bouvet-panther.azurewebsites.net/#/registerBag/";
            var qrCode = baseUrl + bagId;

            var generator = new QRCoder.QRCodeGenerator();
            var qRCodeData = generator.CreateQrCode(qrCode, QRCoder.QRCodeGenerator.ECCLevel.Q);
            var qRCoder = new QRCoder.SvgQRCode(qRCodeData);
            var resultat = qRCoder.GetGraphic(5);

            try
            {
                await bagRepository.AddNewAsync(bagId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return Ok(resultat);
        }
    }
}
