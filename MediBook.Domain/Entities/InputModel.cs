using System.ComponentModel.DataAnnotations;

namespace MediBook.Domain.Entities;

public sealed class InputModel
{
    [Required(ErrorMessage = "Informe o email do usuário")]
    [EmailAddress(ErrorMessage = "O campo email não é um endereço de email válido")]
    [Display(Name = "Email")]
    public string Email { get; set; } = "";

    [Required(ErrorMessage = "Informe a nova senha")]
    [StringLength(100, ErrorMessage = "A {0} deve ter no mínimo {2} e no máximo {1} caracteres.", MinimumLength = 6)]
    [DataType(DataType.Password)]
    [Display(Name = "Senha")]
    public string Password { get; set; } = "";

    [DataType(DataType.Password)]
    [Display(Name = "Confirmação da senha")]
    [Compare("Password", ErrorMessage = "A senha e a senha de confirmação não coincidem.")]
    public string ConfirmPassword { get; set; } = "";

    [Required(ErrorMessage = "Informe a senha atual")]
    [DataType(DataType.Password)]
    [Display(Name = "Senha atual")]
    public string OldPassword { get; set; } = "";
}
