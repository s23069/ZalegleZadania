
using APBD8_DK.Models;
using APBD8_DK.Models.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace APBD8_DK.Services
{
    public class PrescriptionDbService : IPrescriptionDbService
    {

        private readonly MainDbContext _mainDbContext;
        public PrescriptionDbService(MainDbContext mainDbContext)
        {
            _mainDbContext = mainDbContext;
        }
        public async  Task<PrescriptionDTO> GetPrescription(int id)
        {
            if ((await _mainDbContext.Prescriptions.FirstOrDefaultAsync(e => e.IdPrescription == id)) == null)
                throw new Exception("This prescription doesn't exist");


            var prescription = await _mainDbContext.Prescriptions
                .Where(e => e.IdPrescription == id)
                .Include(e => e.Patient)
                .Include(e => e.Doctor)
                .Select(e => new PrescriptionDTO
                {
                    Date = e.Date,
                    DueDate = e.DueDate,
                    Patient = new PatientDTO
                    {
                        FirstName = e.Patient.FirstName,
                        LastName = e.Patient.LastName
                    },
                    Doctor = new DoctorDTO
                    {
                        FirstName = e.Doctor.FirstName,
                        LastName = e.Doctor.LastName,
                        Email = e.Doctor.Email
                    },
                    Medicaments = e.Prescription_Medicaments
                    .Select(m => new MedicamentDTO
                    {
                        Name = m.Medicament.Name,
                        Description = m.Medicament.Description,
                        Type = m.Medicament.Type,
                        Dose = m.Dose,
                        Details = m.Details
                    }).ToList()


                }
                ).FirstOrDefaultAsync() ;
            return prescription;
          
        }
    }
}
