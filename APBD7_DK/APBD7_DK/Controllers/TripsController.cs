using APBD7_DK.Models.DTO;
using APBD7_DK.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace APBD7_DK.Controllers
{
    [Route("api/trips")]
    [ApiController]
    public class TripController : ControllerBase
    {
        private readonly ITripDbService _dbService;

        public TripController(ITripDbService dbService)
        {
            _dbService = dbService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _dbService.GetTrips());
        }
        [HttpPost("{idTrip}/clients")]
        public async Task<IActionResult> PostClientTrip(int idTrip, ClientPOST clientPOST)
        {
            clientPOST.IdTrip = idTrip;
            _dbService.RegisterClient(clientPOST);



            if (await _dbService.ClientAlreadyRegistered(clientPOST))
                return BadRequest("This client is already registred");
            else if (!(await _dbService.TripExists(clientPOST)))
                return BadRequest("This trip does not exists");
            else
            {
                _dbService.CreateClientTrip(clientPOST);
                return Ok("Registered new client");
            }
        }

    }
}

