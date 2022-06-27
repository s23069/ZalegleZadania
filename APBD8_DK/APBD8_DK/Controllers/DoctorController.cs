using APBD8_DK.Models.DTOs;
using APBD8_DK.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace APBD8_DK.Controllers
{
    [Route("doctors")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorDbService _doctorDbService;

        public DoctorController(IDoctorDbService doctorDbService)
        {
            _doctorDbService = doctorDbService;
        }


        [HttpGet("{idDoctor}")]
        public async Task<IActionResult> GetDoctor(int idDoctor)
        {
            try
            {
                return Ok(await _doctorDbService.GetDoctor(idDoctor));
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }



        }

        [HttpPost]
        public async Task<IActionResult> PostDoctor(DoctorDTO doctor)
        {
            await _doctorDbService.AddDoctor(doctor);
            return Ok("Added new Doctor into database");
        }

        [HttpPut("{idDoctor}")]
        public async Task<IActionResult> PutDoctor(int idDoctor, DoctorDTO doctor)
        {
            try
            {
                await _doctorDbService.UpdateDoctor(idDoctor, doctor);
                return Ok("Doctor updated");
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpDelete("{idDoctor}")]

        public async Task<IActionResult> DeleteDoctor(int idDoctor)
        {
            try
            {
                await _doctorDbService.DeleteDoctor(idDoctor);
                return Ok("Doctor deleted from database");
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }


    }
}
