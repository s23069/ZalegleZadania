
using APBD8_DK.Models;
using APBD8_DK.Models.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;


namespace APBD8_DK.Services
{
    public class DoctorDbService : IDoctorDbService
    {
        private readonly MainDbContext _mainDbContext;

        public DoctorDbService(MainDbContext mainContext)
        {
            _mainDbContext = mainContext;

        }

        public async Task AddDoctor(DoctorDTO doctor)
        {
             await _mainDbContext.Doctors.AddAsync(new Doctor
            {
                FirstName = doctor.FirstName,
                LastName = doctor.LastName,
                Email = doctor.Email
            });
            _mainDbContext.SaveChanges();
        }

        public async Task DeleteDoctor(int id)
        {
            var doctor = await _mainDbContext.Doctors.SingleOrDefaultAsync(e=>e.IdDoctor==id);
            if (doctor == null)
                throw new Exception("This doctor doesn't exist");

            _mainDbContext.Doctors.Remove(doctor);
            _mainDbContext.SaveChanges();
        }

        public async Task<DoctorDTO> GetDoctor(int id)
        {

            var doctor =  await _mainDbContext.Doctors.FirstOrDefaultAsync(e => e.IdDoctor == id);
            if (doctor == null)
                throw new Exception("This doctor doesn't exist");

            return new DoctorDTO
            {
                FirstName = doctor.FirstName,
                LastName = doctor.LastName,
                Email = doctor.Email
            };
        }

        public async Task UpdateDoctor(int id, DoctorDTO updateDoctor)
        {
           var doctor = await _mainDbContext.Doctors.SingleOrDefaultAsync(e => e.IdDoctor == id);
            if (doctor == null)
                throw new Exception("This doctor doesn't exist");


            doctor.FirstName = updateDoctor.FirstName;
            doctor.LastName = updateDoctor.LastName;
            doctor.Email = updateDoctor.Email;

            _mainDbContext.SaveChanges();



        }
    }
}
