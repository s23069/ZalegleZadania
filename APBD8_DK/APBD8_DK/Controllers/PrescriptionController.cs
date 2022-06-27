using APBD8_DK.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace APBD8_DK.Controllers
{
    [Route("prescriptions")]
    [ApiController]
    public class PrescriptionController : ControllerBase
    {
        private readonly IPrescriptionDbService _prescriptionDbService;

        public PrescriptionController(IPrescriptionDbService prescriptionDbService)
        {
            _prescriptionDbService = prescriptionDbService;
        }

        [HttpGet("{idPrescription}")]
        public async Task<IActionResult> GetPrescription(int idPrescription)
        {
            try
            {
                return Ok(await _prescriptionDbService.GetPrescription(idPrescription));
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }

        }




    }
}
