using Clientes.Domain.Entities;
using Clientes.Domain.ValueObjects;
using FluentAssertions;
using Xunit;

namespace Clientes.Tests.Domain.Entities
{
    public class ClienteTests
    {
        [Fact]
        public void DeveCriarClienteComDadosValidos()
        {
            // Arrange
            var cpf = new Cpf("12345678909");
            var nomeCompleto = "João da Silva Santos";
            var dataNascimento = new DateTime(1990, 3, 15);
            var email = new Email("joao.silva@email.com");
            var telefone = "11987654321";

            // Act
            var cliente = new Cliente(
                cpf: cpf,
                nomeCompleto: nomeCompleto,
                dataNascimento: dataNascimento,
                email: email,
                telefone: telefone
            );

            // Assert
            cliente.Should().NotBeNull();
            cliente.Cpf.Should().Be(cpf);
            cliente.NomeCompleto.Should().Be(nomeCompleto);
            cliente.DataNascimento.Should().Be(dataNascimento);
            cliente.Email.Should().Be(email);
            cliente.Telefone.Should().Be(telefone);
            cliente.Status.Should().Be("Ativo");
            cliente.DataCadastro.Should().BeCloseTo(DateTime.Now, TimeSpan.FromSeconds(1));
        }

        [Fact]
        public void DeveRejeitarClienteMenorDe16Anos()
        {
            // Arrange
            var cpf = new Cpf("12345678909");
            var nomeCompleto = "Menor de Idade";
            var dataNascimento = DateTime.Now.AddYears(-15); // Menor de 16 anos
            var email = new Email("menor@email.com");
            var telefone = "11987654321";

            // Act
            Action act = () => new Cliente(
                cpf: cpf,
                nomeCompleto: nomeCompleto,
                dataNascimento: dataNascimento,
                email: email,
                telefone: telefone
            );

            // Assert
            act.Should().Throw<ArgumentException>()
            .WithMessage("Cliente deve ter no mínimo 16 anos.");
        }
    }
}