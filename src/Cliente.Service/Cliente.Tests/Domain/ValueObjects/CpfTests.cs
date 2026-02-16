using Cliente.Domain.ValueObjects;
using FluentAssertions;
using Xunit;

namespace Cliente.Tests.Domain.ValueObjects;

public class CpfTests
{
    [Fact]
    public void DeveCriarCpfValido()
    {
        // Arrange
        var cpfValido = "12345678909"; // CPF válido com dígitos verificadores corretos

        // Act
        var cpf = new Cpf(cpfValido);

        // Assert
        cpf.Should().NotBeNull();
        cpf.Valor.Should().Be(cpfValido);
    }

    [Theory]
    [InlineData("00000000000")]
    [InlineData("11111111111")]
    [InlineData("22222222222")]
    [InlineData("33333333333")]
    [InlineData("44444444444")]
    [InlineData("55555555555")]
    [InlineData("66666666666")]
    [InlineData("77777777777")]
    [InlineData("88888888888")]
    [InlineData("99999999999")]
    public void DeveRejeitarCpfComDigitosRepetidos(string cpfInvalido)
    {
        // Act
        Action act = () => new Cpf(cpfInvalido);

        // Assert
        act.Should().Throw<ArgumentException>()
            .WithMessage("CPF inválido: não pode conter dígitos repetidos. (Parameter 'cpf')");
    }

    [Theory]
    [InlineData("123")]           // Muito curto
    [InlineData("123456789")]     // 9 dígitos
    [InlineData("1234567890123")] // 13 dígitos
    public void DeveRejeitarCpfComTamanhoInvalido(string cpfInvalido)
    {
        // Act
        Action act = () => new Cpf(cpfInvalido);

        // Assert
        act.Should().Throw<ArgumentException>()
            .WithMessage("CPF inválido: deve conter exatamente 11 dígitos. (Parameter 'cpf')");
    }

    [Theory]
    [InlineData("12345678900")] // Dígitos verificadores incorretos (correto seria 09)
    [InlineData("12345678901")] // Dígitos verificadores incorretos (correto seria 00)
    public void DeveRejeitarCpfComDigitosVerificadoresInvalidos(string cpfInvalido)
    {
        // Act
        Action act = () => new Cpf(cpfInvalido);

        // Assert
        act.Should().Throw<ArgumentException>()
            .WithMessage("CPF inválido: dígitos verificadores incorretos. (Parameter 'cpf')");
    }

    [Fact]
    public void DeveRejeitarCpfNulo()
    {
        // Act
        Action act = () => new Cpf(null!);

        // Assert
        act.Should().Throw<ArgumentNullException>()
            .WithMessage("CPF não pode ser nulo. (Parameter 'valor')");
    }

    [Fact]
    public void DeveRejeitarCpfVazio()
    {
        // Act
        Action act = () => new Cpf(string.Empty);

        // Assert
        act.Should().Throw<ArgumentException>()
            .WithMessage("CPF não pode ser vazio. (Parameter 'valor')");
    }

    [Fact]
    public void DeveSerIgualPorValor()
    {
        // Arrange
        var cpf1 = new Cpf("12345678909");
        var cpf2 = new Cpf("12345678909");

        // Act & Assert
        cpf1.Should().Be(cpf2);
        (cpf1 == cpf2).Should().BeTrue();
    }

    [Fact]
    public void DeveSerDiferentePorValor()
    {
        // Arrange
        var cpf1 = new Cpf("12345678909");
        var cpf2 = new Cpf("98765432100");

        // Act & Assert
        cpf1.Should().NotBe(cpf2);
        (cpf1 != cpf2).Should().BeTrue();
    }

}