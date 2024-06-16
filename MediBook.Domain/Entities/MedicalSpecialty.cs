using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MediBook.Domain.Entities;

public class MedicalSpecialty(string? name)
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid MedicalSpecialtyId { get; set; }

    [Required]
    [StringLength(255)]
    public string? Name { get; set; } = name;

    public bool Active { get; set; }
}
