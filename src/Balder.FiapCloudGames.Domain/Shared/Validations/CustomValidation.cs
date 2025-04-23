using System.ComponentModel.DataAnnotations;

namespace Balder.FiapCloudGames.Domain.Shared.Validations
{
    public static class CustomValidation
    {
        public static ValidationResult PasswordValidate(string password)
        {
            if (password.Length < 8)
                return new ValidationResult("A senha deve ter pelo menos 8 caracteres.");
            if (!password.Any(char.IsUpper))
                return new ValidationResult("A senha deve conter pelo menos uma letra maiúscula.");
            if (!password.Any(char.IsLower))
                return new ValidationResult("A senha deve conter pelo menos uma letra minúscula.");
            if (!password.Any(char.IsDigit))
                return new ValidationResult("A senha deve conter pelo menos um número.");
            if (!password.Any(ch => !char.IsLetterOrDigit(ch)))
                return new ValidationResult("A senha deve conter pelo menos um caractere especial.");
            return ValidationResult.Success!;
        }

    }
}