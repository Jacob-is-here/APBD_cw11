namespace Apbd11.DTOs;

public class PatientDTOs
{
    public int IdPatient { get; set; }
    public String FirstName { get; set; }
    public String LastName { get; set; }
    public DateTime Birthdate { get; set; }
    public List<PrescriptionDTOs> Prescriptions { get; set; }// czemu nie lista
    
}

public class PrescriptionDTOs
{
    public int IdPrescription { get; set; }
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }
    public List<MedicamentDTOs> Medicaments { get; set; }
    public DoctorDTOs Doctor { get; set; }
}


public class MedicamentDTOs
{
    public int IdMedicament { get; set; }
    public string Name { get; set; }
    public int? Dose { get; set; }
    public string Description { get; set; }
}

public class DoctorDTOs
{
    public int IdDoctor { get; set; }
    public string FirstName { get; set; }
}
