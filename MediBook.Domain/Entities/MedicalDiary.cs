using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MediBook.Domain.Entities;

public class MedicalDiary(DateTime dateAndTime, bool active)
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid MedicalDiaryId { get; set; } 

    [ForeignKey("DoctorId")]
    public Guid DoctorId { get; set; } 

    public DateTime DateAndTime { get; set; } = dateAndTime;

    [NotMapped]
    public string? DateDisplay
    {
        get
        {
            if (DateAndTime != DateTime.MinValue)
            {
                return DateAndTime.ToString("dd/MM/yyyy");
            }
            else
            {
                return "";
            }
        }
    }

    [NotMapped]
    public string? TimeDisplay
    {
        get
        {
            if (DateAndTime != DateTime.MinValue)
            {
                return DateAndTime.ToString("HH:mm");
            }
            else
            {
                return "";
            }
        }
    }

    [ForeignKey("MedicalSpecialtyId")]
    public Guid MedicalSpecialtyId { get; set; } 

    public bool Active { get; set; } = active;

    public virtual Doctor Doctor { get; set; } = null!;

    public virtual MedicalSpecialty MedicalSpecialty { get; set; } = null!;

    public virtual Appointment Appointment { get; set; } = null!;
}
