using Clientes.Domain.ValueObjects;
using FluentAssertions;
using Xunit;

namespace Clientes.Tests.Domain.ValueObjects
{
    public class EmailTests
    {
        [Fact]
        public void DeveCriarEmailValido()
        {
            // Arrange
            var emailValido = "joao.silva@email.com";

            // Act
            var email = new Email(emailValido);

            // Assert
            email.Should().NotBeNull();
            email.Valor.Should().Be(emailValido);
        }

        [Fact]
        public void DeveRejeitarEmailNulo()
        {
            // Act
            Action act = () => new Email(null!);

            // Assert
            act.Should().Throw<ArgumentNullException>()
                .WithMessage("O email não pode ser nulo. (Parameter 'valor')");
        }

        [Fact]
        public void DeveRejeitarEmailVazio()
        {
            // Act
            Action act = () => new Email(string.Empty);

            // Assert
            act.Should().Throw<ArgumentException>()
                .WithMessage("O email não pode ser vazio. (Parameter 'valor')");
        }

        [Theory]
        [InlineData("email.invalido")]          // Sem @
        [InlineData("@dominio.com")]            // Sem parte local
        [InlineData("usuario@")]                // Sem domínio
        [InlineData("usuario@dominio")]         // Sem extensão
        [InlineData("usuario dominio.com")]     // Espaço ao invés de @
        [InlineData("usuario@@dominio.com")]    // @ duplo
        public void DeveRejeitarEmailComFormatoInvalido(string emailInvalido)
        {
            // Act
            Action act = () => new Email(emailInvalido);

            // Assert
            act.Should().Throw<ArgumentException>()
                .WithMessage("Email inválido: formato incorreto. (Parameter 'valor')");
        }

        [Fact]
        public void DeveSerIgualPorValor()
        {
            // Arrange
            var email1 = new Email("joao.silva@email.com");
            var email2 = new Email("joao.silva@email.com");

            // Act & Assert
            email1.Should().Be(email2);
            (email1 == email2).Should().BeTrue();
        }

        [Fact]
        public void DeveSerDiferentePorValor()
        {
            // Arrange
            var email1 = new Email("joao.silva@email.com");
            var email2 = new Email("maria.santos@email.com");

            // Act & Assert
            email1.Should().NotBe(email2);
            (email1 != email2).Should().BeTrue();
        }

    }

}