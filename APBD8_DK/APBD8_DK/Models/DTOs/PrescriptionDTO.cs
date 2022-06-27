using System;
using System.Collections;
using System.Collections.Generic;

namespace APBD8_DK.Models.DTOs
{
    public class PrescriptionDTO
    {
        public DateTime Date { get; set; }
        public DateTime DueDate { get; set; }
        public PatientDTO Patient { get; set; }
        public DoctorDTO Doctor { get; set; }
        public ICollection<MedicamentDTO> Medicaments { get; set; }
    }
}
