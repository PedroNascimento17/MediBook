using MediBook.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MediBook.Domain.Entities;

public class Appointment(Guid patientID,
                         Guid medicalDiaryId,
                         bool patientShowedUp, 
                         AppointmentType? appointmentType, 
                         AppointmentStatus? appointmentStatus,
                         bool active)
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid AppointmentId { get; set; } 

    [ForeignKey("PatientID")]
    public Guid PatientID { get; set; } = patientID;

    public Guid MedicalDiaryId { get; set; } = medicalDiaryId;

    public bool PatientShowedUp { get; set; } = patientShowedUp;

    public bool Active { get; set; } = active;

    public AppointmentType? AppointmentType { get; set; } = appointmentType;

    public AppointmentStatus? AppointmentStatus { get; set; } = appointmentStatus;

    public virtual Patient Patient { get; set; } = null!;

    public virtual MedicalDiary MedicalDiary { get; set; } = null!;
}
