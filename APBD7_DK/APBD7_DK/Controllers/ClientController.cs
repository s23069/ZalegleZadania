using APBD7_DK.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace APBD7_DK.Controllers
{
    [Route("api/clients")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientDbService _dbService;

        public ClientController(IClientDbService dbService)
        {
            _dbService = dbService;
        }

        [Route("{idClient}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteClient(int idClient)
        {

            if (await _dbService.ClientExists(idClient))
            {
                _dbService.DeleteClient(idClient);
                return Ok("Deleted client" + idClient);
            }
            return BadRequest("This client cannot be deleted");


        }
    }
}

