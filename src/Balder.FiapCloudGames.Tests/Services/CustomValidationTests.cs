using Balder.FiapCloudGames.Domain.Shared.Validations;
namespace Balder.FiapCloudGames.Tests.Validations;

public class CustomValidationTests
{
    [Fact]
    public void PasswordValidate_ShouldReturnError_WhenPasswordIsTooShort()
    {
        // Arrange
        var password = "Abc1!";

        // Act
        var result = CustomValidation.PasswordValidate(password);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("A senha deve ter pelo menos 8 caracteres.", result.ErrorMessage);
    }

    [Fact]
    public void PasswordValidate_ShouldReturnError_WhenPasswordHasNoUppercase()
    {
        // Arrange
        var password = "abc12345!";

        // Act
        var result = CustomValidation.PasswordValidate(password);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("A senha deve conter pelo menos uma letra maiúscula.", result.ErrorMessage);
    }

    [Fact]
    public void PasswordValidate_ShouldReturnError_WhenPasswordHasNoLowercase()
    {
        // Arrange
        var password = "ABC12345!";

        // Act
        var result = CustomValidation.PasswordValidate(password);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("A senha deve conter pelo menos uma letra minúscula.", result.ErrorMessage);
    }

    [Fact]
    public void PasswordValidate_ShouldReturnError_WhenPasswordHasNoDigit()
    {
        // Arrange
        var password = "Abcdefgh!";

        // Act
        var result = CustomValidation.PasswordValidate(password);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("A senha deve conter pelo menos um número.", result.ErrorMessage);
    }

    [Fact]
    public void PasswordValidate_ShouldReturnError_WhenPasswordHasNoSpecialCharacter()
    {
        // Arrange
        var password = "Abc12345";

        // Act
        var result = CustomValidation.PasswordValidate(password);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("A senha deve conter pelo menos um caractere especial.", result.ErrorMessage);
    }

    [Fact]
    public void PasswordValidate_ShouldReturnSuccess_WhenPasswordIsValid()
    {
        // Arrange
        var password = "Abc12345!";

        // Act
        var result = CustomValidation.PasswordValidate(password);

        // Assert
        Assert.Null(result); // ValidationResult.Success is null
    }
}