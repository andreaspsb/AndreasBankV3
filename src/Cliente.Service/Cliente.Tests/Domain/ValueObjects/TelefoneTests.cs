using Clientes.Domain.ValueObjects;
using FluentAssertions;
using Xunit;

namespace Clientes.Tests.Domain.ValueObjects;

public class TelefoneTests
{
    [Theory]
    [InlineData("1198765432")]   // 10 dígitos (fixo)
    [InlineData("11987654321")]  // 11 dígitos (celular)
    public void DeveCriarTelefoneValido(string telefoneValido)
    {
        // Act
        var telefone = new Telefone(telefoneValido);

        // Assert
        telefone.Should().NotBeNull();
        telefone.Valor.Should().Be(telefoneValido);
    }

    [Fact]
    public void DeveRejeitarTelefoneNulo()
    {
        // Act
        Action act = () => new Telefone(null!);

        // Assert
        act.Should().Throw<ArgumentNullException>()
            .WithMessage("O telefone não pode ser nulo. (Parameter 'valor')");
    }

    [Fact]
    public void DeveRejeitarTelefoneVazio()
    {
        // Act
        Action act = () => new Telefone(string.Empty);

        // Assert
        act.Should().Throw<ArgumentException>()
            .WithMessage("O telefone não pode ser vazio. (Parameter 'valor')");
    }

    [Theory]
    [InlineData("123")]          // Muito curto
    [InlineData("123456789")]    // 9 dígitos
    [InlineData("123456789012")] // 12 dígitos
    public void DeveRejeitarTelefoneComTamanhoInvalido(string telefoneInvalido)
    {
        // Act
        Action act = () => new Telefone(telefoneInvalido);

        // Assert
        act.Should().Throw<ArgumentException>()
            .WithMessage("Telefone inválido: deve conter entre 10 e 11 dígitos. (Parameter 'valor')");
    }

    [Theory]
    [InlineData("1198765-4321")]  // Com hífen
    [InlineData("(11)987654321")] // Com parênteses
    [InlineData("11 987654321")]  // Com espaço
    [InlineData("11ab9876543")]   // Com letras
    public void DeveRejeitarTelefoneComCaracteresInvalidos(string telefoneInvalido)
    {
        // Act
        Action act = () => new Telefone(telefoneInvalido);

        // Assert
        act.Should().Throw<ArgumentException>()
            .WithMessage("Telefone inválido: deve conter apenas dígitos. (Parameter 'valor')");
    }

    [Fact]
    public void DeveSerIgualPorValor()
    {
        // Arrange
        var telefone1 = new Telefone("11987654321");
        var telefone2 = new Telefone("11987654321");

        // Act & Assert
        telefone1.Should().Be(telefone2);
        (telefone1 == telefone2).Should().BeTrue();
    }

    [Fact]
    public void DeveSerDiferentePorValor()
    {
        // Arrange
        var telefone1 = new Telefone("11987654321");
        var telefone2 = new Telefone("11912345678");

        // Act & Assert
        telefone1.Should().NotBe(telefone2);
        (telefone1 != telefone2).Should().BeTrue();
    }
}