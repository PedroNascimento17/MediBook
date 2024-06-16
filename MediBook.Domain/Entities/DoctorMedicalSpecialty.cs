using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MediBook.Domain.Entities;

public class DoctorMedicalSpecialty(Guid doctorId, Guid medicalSpecialtyId, bool active)
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid DoctorMedicalSpecialtyId { get; set; }

    [ForeignKey("DoctorId")]
    public Guid DoctorId { get; set; } = doctorId;

    [ForeignKey("MedicalSpecialtyId")]
    public Guid MedicalSpecialtyId { get; set; } = medicalSpecialtyId;

    public bool Active { get; set; } = active;

    public virtual MedicalSpecialty MedicalSpecialty { get; set; } = null!;

    public virtual Doctor Doctor { get; set; } = null!;
}
