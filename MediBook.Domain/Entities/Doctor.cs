using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MediBook.Domain.Entities;

public class Doctor(string? name, string? medicalRegistration, bool active, string? applicationUserId)
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid DoctorId { get; set; } 

    [Required(ErrorMessage = "Nome do médico é obrigatório")]
    [StringLength(255)]
    public string? Name { get; set; } = name;

    [Required(ErrorMessage = "Registro do médico é obrigatório")]
    [StringLength(50)]
    public string? MedicalRegistration { get; set; } = medicalRegistration;

    public bool Active { get; set; } = active;

    [ForeignKey("ApplicationUserId")]
    public string? ApplicationUserId { get; set; } = applicationUserId;

    public virtual ApplicationUser ApplicationUser { get; set; } = null!;

    public virtual IEnumerable<DoctorMedicalSpecialty> DoctorMedicalSpecialty { get; set; } = null!;

    [NotMapped]
    public InputModel User { get; set; } = new InputModel();
}
