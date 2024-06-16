using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MediBook.Domain.Entities;

public class Patient(string? name, 
                     string? cpf, 
                     string? zipCode, 
                     int addressNumber, 
                     string? addressComplement, 
                     DateTime? birthDate,
                     string gender, 
                     bool active,
                     string? applicationUserId)
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid PatientID { get; set; } 

    [Required(ErrorMessage = "Nome do paciente é obrigatório")]
    [StringLength(255, ErrorMessage = "Tamanho máximo do Nome é 255")]
    public string? Name { get; set; } = name;

    [StringLength(11, ErrorMessage = "Tamanho máximo do CPF é 8")]
    public string? Cpf { get; set; } = cpf;

    [StringLength(8, ErrorMessage = "Tamanho máximo do CEP é 8")]
    public string? ZipCode { get; set; } = zipCode;

    public int AddressNumber { get; set; } = addressNumber;

    [StringLength(255, ErrorMessage = "Tamanho máximo do Complemento é 255")]
    public string? AddressComplement { get; set; } = addressComplement;

    public DateTime? BirthDate { get; set; } = birthDate;

    [NotMapped]
    public string? BirthDateDisplay 
    {
        get
        {
            if (BirthDate != null && BirthDate != DateTime.MinValue)
            {
                return BirthDate.Value.ToString("dd/MM/yyyy");
            }
            else
            {
                return "";
            }
        }
    }

    [StringLength(1, ErrorMessage = "Tamanho máximo do Sexo é 1")]
    public string Gender { get; set; } = gender;

    public bool Active { get; set; } = active;

    [ForeignKey("ApplicationUserId")]
    public string? ApplicationUserId { get; set; } = applicationUserId;

    public virtual ApplicationUser ApplicationUser { get; set; } = null!;

    [NotMapped]
    public InputModel User { get; set; } = new InputModel();
}
