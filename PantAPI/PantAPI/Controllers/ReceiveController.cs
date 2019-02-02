using Microsoft.AspNetCore.Mvc;
using PantAPI.Models;
using PantAPI.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PantAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReceiveController: ControllerBase
    {
        private BagRepository bagRepository;

        public ReceiveController(BagRepository bagRepository)
        {
            this.bagRepository = bagRepository;
        }

        [HttpPost]
        [Route("Receive")]
        [ProducesResponseType(typeof(ReceiveResultModel), 200)]
        public async Task<ActionResult> Receive([FromBody] ReceiveModel receiveModel)
        {
            var bag = await bagRepository.GetAsync("NA", receiveModel.BagId);
            if (bag == null)
            {
                return Ok(new ReceiveResultModel
                {
                    Status = ReceiveStatus.UNKNOWN,
                    BagId =  receiveModel.BagId
                });
            }

            bag.Value = receiveModel.Value;
            bag.Status = BagStatus.Recieved;
            bag.ReceiveDate = DateTime.UtcNow;
            bag.ReceiveLocation = receiveModel.Location;
            bag.Weight = receiveModel.Weight;

            await bagRepository.AddOrUpdateAsync(bag);

            return Ok(new ReceiveResultModel
            {
                Status = ReceiveStatus.OK,
                BagId = bag.BagId,
            });
        }

    }
}
