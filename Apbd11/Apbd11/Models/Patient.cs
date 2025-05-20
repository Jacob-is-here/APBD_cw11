using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Apbd11.Models;

[Table("Patient")]
public class Patient
{
    [Key]
    public int IdPatient { get; set; }
    [MaxLength(100)]
    public String FirstName { get; set; }
    [MaxLength(100)]
    public String LastName { get; set; }
    public DateTime Birthdate { get; set; }
    public ICollection<Prescription> Prescriptions { get; set; }
}