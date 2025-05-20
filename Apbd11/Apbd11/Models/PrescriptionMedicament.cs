using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;

namespace Apbd11.Models;
[PrimaryKey(nameof(IdPrescription), nameof(IdMedicament))]
[Table("Prescription_Medicament")]
public class PrescriptionMedicament
{
    [ForeignKey(nameof(Medicament))]
    public int IdMedicament { get; set; }
    [ForeignKey(nameof(Prescription))]
    public int IdPrescription { get; set; }
    // [Nullable()] ???
    public int? Dose { get; set; }
    [MaxLength(100)]
    public String Details { get; set; }
}