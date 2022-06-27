using Microsoft.EntityFrameworkCore;
using System;

namespace APBD8_DK.Models
{
    public class MainDbContext : DbContext
    {


        protected MainDbContext()
        {

        }

        public MainDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }

        internal DateTime FirstOrDefault()
        {
            throw new NotImplementedException();
        }

        public DbSet<Medicament> Medicaments { get; set; }
        public DbSet<Prescription_Medicament> Prescription_Medicaments { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Prescription_Medicament>(entity =>
            {
                entity.HasKey(e => new { e.IdMedicament, e.IdPrescription });

            });

            SeedData(modelBuilder);


        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Prescription_Medicament>()
               .HasData(
                   new Prescription_Medicament
                   {
                       IdPrescription = 1,
                       IdMedicament = 1,
                       Details = "overdose",
                       Dose = 4,
                   }
               );
            modelBuilder.Entity<Prescription>()
             .HasData(
                 new Prescription
                 {
                     IdPrescription = 1,
                     IdDoctor = 1,
                     IdPatient = 2,
                     Date = DateTime.Now,
                     DueDate = DateTime.Now.AddDays(1)
                 }
             );
            modelBuilder.Entity<Medicament>()
               .HasData(
                   new Medicament
                   {
                       IdMedicament = 1,
                       Name = "lek 1",
                       Description = "bol glowy",
                       Type = "Typ 1"
                   },
                   new Medicament
                   {
                       IdMedicament = 2,
                       Name = "lek 2",
                       Description = "bol miesni",
                       Type = "Typ 2"
                   });

            modelBuilder.Entity<Patient>()
               .HasData(
                   new Patient
                   {
                       IdPatient = 1,
                       FirstName = "Adrian",
                       LastName = "Nowak",
                       BirthDate = Convert.ToDateTime("1978-05-15T00:00:00")
                   },
                   new Patient
                   {
                       IdPatient = 2,
                       FirstName = "Pawel",
                       LastName = "Kowalski",
                       BirthDate = Convert.ToDateTime("1980-05-20T00:00:00")
                   });

            modelBuilder.Entity<Doctor>()
               .HasData(
                   new Doctor
                   {
                       IdDoctor = 1,
                       FirstName = "Mariusz",
                       LastName = "Kowalczyk",
                       Email = "mk@wp.pl"
                   },
                   new Doctor
                   {
                       IdDoctor = 2,
                       FirstName = "Lukasz",
                       LastName = "Koc",
                       Email = "kocyk@wp.pl"
                   });

        }

    }
}
