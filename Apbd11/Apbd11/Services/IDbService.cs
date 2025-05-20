using Apbd11.DTOs;

namespace Apbd11.Services;

public interface IDbService
{
    Task Create(PrescriptionDTO prescriptionDto , CancellationToken cancellationToken);
    Task<PatientDTOs> GetPacjent(int id);
}