using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Apbd11.Models;
[Table("Doctor")]
public class Doctor
{
    [Key]
    public int IdDoctor { get; set; }
    [MaxLength(100)]
    public String FirstName { get; set; }
    [MaxLength(100)]
    public String LastName { get; set; }
    [MaxLength(100)]
    public String Email { get; set; }
    public ICollection<Prescription> Prescriptions { get; set; }
}