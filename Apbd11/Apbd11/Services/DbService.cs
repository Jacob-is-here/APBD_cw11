using System.ComponentModel.DataAnnotations;
using Apbd11.DTOs;
using Apbd11.Models;
using Microsoft.Data.SqlClient;
using System.Data.Common;
using System.Diagnostics.Metrics;
using APBD_example_test1_2025.Exceptions;
using Apbd11.Data;
using Microsoft.EntityFrameworkCore;


namespace Apbd11.Services;


public class DbService : IDbService
{
   private readonly DatabaseContext _context;

    public DbService(DatabaseContext context)
    {
        _context = context;
    }
    
    public async Task Create(PrescriptionDTO prescriptionDto, CancellationToken cancellationToken)
{
    if (prescriptionDto.Medicaments.Count > 10)
        throw new ValidationException("Nie można dodać więcej niż 10 leków do jednej recepty.");

    if (prescriptionDto.DueDate < prescriptionDto.Date)
        throw new ValidationException("Data wygaśnięcia recepty nie może być wcześniejsza niż data wystawienia.");

    var doctor = await _context.Doctors.Where(a => a.IdDoctor == prescriptionDto.Doctor.IdDoctor).CountAsync();
    if (doctor == 0)
    {
        throw new ValidationException("Lekarz nie istnieje");    
    }

    var leki = await _context.Medicaments.Where(m => prescriptionDto.Medicaments.Select(s => s.IdMedicament)
        .Contains(m.IdMedicament)).CountAsync();
    
    if (leki != prescriptionDto.Medicaments.Count)
        throw new ValidationException("Niektóre leki nie istnieją w bazie danych.");

    var person = await _context.Patients.CountAsync(a => a.IdPatient == prescriptionDto.Patient.IdPatient);
    if (person == 0)
    {
        var patient = new Patient
        {
            IdPatient = prescriptionDto.Patient.IdPatient,
            FirstName = prescriptionDto.Patient.FirstName,
            LastName = prescriptionDto.Patient.LastName,
            Birthdate = prescriptionDto.Patient.Birthdate
        };
        await _context.Patients.AddAsync(patient, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

    }

    var prescription = new Prescription()
    {
        IdPatient = prescriptionDto.Patient.IdPatient,
        Date = prescriptionDto.Date,
        DueDate = prescriptionDto.DueDate,
        IdDoctor = prescriptionDto.Doctor.IdDoctor,
        PrescriptionMedicaments = prescriptionDto.Medicaments.Select(m => new PrescriptionMedicament
        {
            IdMedicament = m.IdMedicament,
            Dose = m.Dose,
            Details = m.Description
        }).ToList()
    };

    await _context.Prescriptions.AddAsync(prescription, cancellationToken);
    await _context.SaveChangesAsync(cancellationToken);


}

    public async Task<PatientDTOs> GetPacjent(int id)
    {
        var pacjent = await _context.Patients
            .Where(e => e.IdPatient == id)
            .Select(e =>
            new PatientDTOs()
            {
                IdPatient = e.IdPatient,
                FirstName = e.FirstName,
                LastName = e.LastName,
                Birthdate = e.Birthdate,
                Prescriptions = _context.Prescriptions
                    .Where(q =>q.IdPatient == e.IdPatient).OrderBy(a => a.DueDate)
                    .Select(a =>
                    new PrescriptionDTOs
                    {
                        IdPrescription = a.IdPrescription,
                        Date = a.Date,
                        Medicaments = _context.PrescriptionMedicaments.Where(s => s.IdPrescription == a.IdPrescription).
                            Select(s =>
                            new MedicamentDTOs
                            {
                                IdMedicament = s.IdMedicament,
                                Name = _context.Medicaments
                                    .Where(m => m.IdMedicament == s.IdMedicament)
                                    .Select(m => m.Name).FirstOrDefault() ?? string.Empty,
                                Dose = s.Dose,
                                Description = _context.Medicaments
                                    .Where(m => m.IdMedicament == s.IdMedicament)
                                    .Select(m => m.Description).FirstOrDefault() ?? string.Empty,
                            }).ToList(),
                        Doctor = new DoctorDTOs
                        {
                        IdDoctor = a.IdDoctor,
                        FirstName = _context.Doctors.Where(d =>d.IdDoctor == a.IdDoctor).
                            Select(d => d.FirstName).FirstOrDefault() ?? string.Empty
                    }
                    }).ToList()
            }).FirstOrDefaultAsync();
        return pacjent;
    }
}