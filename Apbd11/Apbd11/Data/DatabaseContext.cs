using Apbd11.Models;
using Microsoft.EntityFrameworkCore;

namespace Apbd11.Data;

public class DatabaseContext : DbContext
{
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Medicament> Medicaments { get; set; }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Prescription> Prescriptions { get; set; }
    public DbSet<PrescriptionMedicament> PrescriptionMedicaments { get; set; }
    
    protected DatabaseContext()
    {
    }

    public DatabaseContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Patient>().HasData(new List<Patient>
    {
        new Patient { IdPatient = 1, FirstName = "Jan", LastName = "Kowalski", Birthdate = new DateTime(1980, 5, 15) },
        new Patient { IdPatient = 2, FirstName = "Anna", LastName = "Nowak", Birthdate = new DateTime(1990, 3, 10) },
        new Patient { IdPatient = 3, FirstName = "Piotr", LastName = "Wiśniewski", Birthdate = new DateTime(1975, 7, 20) },
        new Patient { IdPatient = 4, FirstName = "Maria", LastName = "Zielińska", Birthdate = new DateTime(2000, 1, 5) }
    });

    modelBuilder.Entity<Doctor>().HasData(new List<Doctor>
    {
        new Doctor { IdDoctor = 1, FirstName = "Adam", LastName = "Lewandowski", Email = "adam.lewandowski@example.com" },
        new Doctor { IdDoctor = 2, FirstName = "Ewa", LastName = "Kaczmarek", Email = "ewa.kaczmarek@example.com" },
        new Doctor { IdDoctor = 3, FirstName = "Tomasz", LastName = "Mazur", Email = "tomasz.mazur@example.com" },
        new Doctor { IdDoctor = 4, FirstName = "Katarzyna", LastName = "Wójcik", Email = "katarzyna.wojcik@example.com" }
    });

    modelBuilder.Entity<Medicament>().HasData(new List<Medicament>
    {
        new Medicament { IdMedicament = 1, Name = "Lek na ból głowy", Description = "Przeciwbólowy", Type = "AAA"},
        new Medicament { IdMedicament = 2, Name = "Antybiotyk", Description = "Na infekcje" ,Type = "BBB"},
        new Medicament { IdMedicament = 3, Name = "Witamina C",  Description = "Suplement diety" ,Type = "CCC"},
        new Medicament { IdMedicament = 4, Name = "Lek na alergię", Description = "Przeciwalergiczny" ,Type = "DDD"}
    });

    modelBuilder.Entity<Prescription>().HasData(new List<Prescription>
    {
        new Prescription { IdPrescription = 1, IdPatient = 1, IdDoctor = 1, Date = new DateTime(2023, 10, 1), DueDate = new DateTime(2023, 10, 15) },
        new Prescription { IdPrescription = 2, IdPatient = 2, IdDoctor = 2, Date = new DateTime(2023, 9, 20), DueDate = new DateTime(2023, 10, 5) },
        new Prescription { IdPrescription = 3, IdPatient = 3, IdDoctor = 3, Date = new DateTime(2023, 8, 15), DueDate = new DateTime(2023, 9, 1) },
        new Prescription { IdPrescription = 4, IdPatient = 4, IdDoctor = 4, Date = new DateTime(2023, 7, 10), DueDate = new DateTime(2023, 7, 25) }
    });

    modelBuilder.Entity<PrescriptionMedicament>().HasData(new List<PrescriptionMedicament>
    {
        new PrescriptionMedicament { IdPrescription = 1, IdMedicament = 1, Dose = 2, Details = "Stosować rano i wieczorem" },
        new PrescriptionMedicament { IdPrescription = 1, IdMedicament = 2, Dose = 1, Details = "Stosować raz dziennie" },
        new PrescriptionMedicament { IdPrescription = 2, IdMedicament = 3, Dose = null, Details = "Według potrzeby" },
        new PrescriptionMedicament { IdPrescription = 2, IdMedicament = 4, Dose = 3, Details = "Stosować przed snem" }
    });
    }
}