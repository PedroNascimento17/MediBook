using Microsoft.AspNetCore.Identity;

namespace MediBook.Application.Mappers;
public static class IdentityErrorMapper
{
    public static IdentityError MapToPortugueseIdentityError(IdentityError identityError)
    {
        return identityError.Code switch
        {
            "ConcurrencyFailure" => ConcurrencyFailure(),
            "PasswordMismatch" => PasswordMismatch(),
            "InvalidToken" => InvalidToken(),
            "LoginAlreadyAssociated" => LoginAlreadyAssociated(),
            "InvalidUserName" => InvalidUserName(),
            "InvalidEmail" => InvalidEmail(),
            "DuplicateUserName" => DuplicateUserName(),
            "DuplicateEmail" => DuplicateEmail(),
            "InvalidRoleName" => InvalidRoleName(),
            "DuplicateRoleName" => DuplicateRoleName(),
            "UserAlreadyHasPassword" => UserAlreadyHasPassword(),
            "UserLockoutNotEnabled" => UserLockoutNotEnabled(),
            "UserAlreadyInRole" => UserAlreadyInRole(),
            "UserNotInRole" => UserNotInRole(),
            "PasswordTooShort" => PasswordTooShort(),
            "PasswordRequiresNonAlphanumeric" => PasswordRequiresNonAlphanumeric(),
            "PasswordRequiresDigit" => PasswordRequiresDigit(),
            "PasswordRequiresLower" => PasswordRequiresLower(),
            "PasswordRequiresUpper" => PasswordRequiresUpper(),
            _ => DefaultError(),
        };
    }

    private static IdentityError DefaultError() { return new IdentityError { Code = nameof(DefaultError), Description = $"Um erro inesperado aconteceu." }; }
    private static IdentityError ConcurrencyFailure() { return new IdentityError { Code = nameof(ConcurrencyFailure), Description = "Falha de simultaneidade otimista, o objeto foi modificado." }; }
    private static IdentityError PasswordMismatch() { return new IdentityError { Code = nameof(PasswordMismatch), Description = "Senha incorreta." }; }
    private static IdentityError InvalidToken() { return new IdentityError { Code = nameof(InvalidToken), Description = "Token inválido." }; }
    private static IdentityError LoginAlreadyAssociated() { return new IdentityError { Code = nameof(LoginAlreadyAssociated), Description = "Já existe um usuário com este email." }; }
    private static IdentityError InvalidUserName() { return new IdentityError { Code = nameof(InvalidUserName), Description = $"Nome de usuário inválido, só pode conter letras ou dígitos." }; }
    private static IdentityError InvalidEmail() { return new IdentityError { Code = nameof(InvalidEmail), Description = $"Email inválido." }; }
    private static IdentityError DuplicateUserName() { return new IdentityError { Code = nameof(DuplicateUserName), Description = $"Nome de usuário já está sendo utilizado." }; }
    private static IdentityError DuplicateEmail() { return new IdentityError { Code = nameof(DuplicateEmail), Description = $"Email já está sendo utilizado." }; }
    private static IdentityError InvalidRoleName() { return new IdentityError { Code = nameof(InvalidRoleName), Description = $"Função inválida." }; }
    private static IdentityError DuplicateRoleName() { return new IdentityError { Code = nameof(DuplicateRoleName), Description = $"Nome da função já está sendo utilizado." }; }
    private static IdentityError UserAlreadyHasPassword() { return new IdentityError { Code = nameof(UserAlreadyHasPassword), Description = "O usuário já tem uma senha definida." }; }
    private static IdentityError UserLockoutNotEnabled() { return new IdentityError { Code = nameof(UserLockoutNotEnabled), Description = "O bloqueio não está ativado para este usuário." }; }
    private static IdentityError UserAlreadyInRole() { return new IdentityError { Code = nameof(UserAlreadyInRole), Description = $"Usuário já está na função." }; }
    private static IdentityError UserNotInRole() { return new IdentityError { Code = nameof(UserNotInRole), Description = $"Usuário não está na função." }; }
    private static IdentityError PasswordTooShort() { return new IdentityError { Code = nameof(PasswordTooShort), Description = $"Senha muito curta." }; }
    private static IdentityError PasswordRequiresNonAlphanumeric() { return new IdentityError { Code = nameof(PasswordRequiresNonAlphanumeric), Description = "As senhas devem ter pelo menos um caractere não alfanumérico." }; }
    private static IdentityError PasswordRequiresDigit() { return new IdentityError { Code = nameof(PasswordRequiresDigit), Description = "As senhas devem ter pelo menos um dígito ('0'-'9')." }; }
    private static IdentityError PasswordRequiresLower() { return new IdentityError { Code = nameof(PasswordRequiresLower), Description = "As senhas devem ter pelo menos uma letra minúscula ('a'-'z')." }; }
    private static IdentityError PasswordRequiresUpper() { return new IdentityError { Code = nameof(PasswordRequiresUpper), Description = "As senhas devem ter pelo menos uma letra maiúscula ('A'-'Z')." }; }
}
