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
            var telefone = new Telefone("11987654321");

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
            var telefone = new Telefone("11987654321");

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

        [Fact]
        public void DeveRejeitarNomeCompletoNulo()
        {
            // Arrange
            var cpf = new Cpf("12345678909");
            var dataNascimento = new DateTime(1990, 3, 15);
            var email = new Email("joao.silva@email.com");
            var telefone = new Telefone("11987654321");

            // Act
            Action act = () => new Cliente(
                cpf: cpf,
                nomeCompleto: null, // Nome completo nulo
                dataNascimento: dataNascimento,
                email: email,
                telefone: telefone
            );

            // Assert
            act.Should().Throw<ArgumentNullException>()
                .WithMessage("O nome completo não pode ser nulo. (Parameter 'nomeCompleto')");
        }

        [Fact]
        public void DeveRejeitarNomeCompletoVazio()
        {
            // Arrange
            var cpf = new Cpf("12345678909");
            var dataNascimento = new DateTime(1990, 3, 15);
            var email = new Email("joao.silva@email.com");
            var telefone = new Telefone("11987654321");

            // Act
            Action act = () => new Cliente(
                cpf: cpf,
                nomeCompleto: string.Empty, // Nome completo vazio
                dataNascimento: dataNascimento,
                email: email,
                telefone: telefone
            );

            // Assert
            act.Should().Throw<ArgumentException>()
                .WithMessage("O nome completo não pode ser vazio. (Parameter 'nomeCompleto')");
        }

        [Fact]
        public void DeveRejeitarCpfNulo()
        {
            // Arrange
            var nomeCompleto = "João da Silva Santos";
            var dataNascimento = new DateTime(1990, 3, 15);
            var email = new Email("joao.silva@email.com");
            var telefone = new Telefone("11987654321");

            // Act
            Action act = () => new Cliente(
                cpf: null!,
                nomeCompleto: nomeCompleto,
                dataNascimento: dataNascimento,
                email: email,
                telefone: telefone
            );

            // Assert
            act.Should().Throw<ArgumentNullException>()
                .WithMessage("*cpf*");
        }

        [Fact]
        public void DeveRejeitarEmailNulo()
        {
            // Arrange
            var cpf = new Cpf("12345678909");
            var nomeCompleto = "João da Silva Santos";
            var dataNascimento = new DateTime(1990, 3, 15);
            var telefone = new Telefone("11987654321");

            // Act
            Action act = () => new Cliente(
                cpf: cpf,
                nomeCompleto: nomeCompleto,
                dataNascimento: dataNascimento,
                email: null!,
                telefone: telefone
            );

            // Assert
            act.Should().Throw<ArgumentNullException>()
                .WithMessage("*email*");
        }

        [Fact]
        public void DeveRejeitarTelefoneNulo()
        {
            // Arrange
            var cpf = new Cpf("12345678909");
            var nomeCompleto = "João da Silva Santos";
            var dataNascimento = new DateTime(1990, 3, 15);
            var email = new Email("joao.silva@email.com");

            // Act
            Action act = () => new Cliente(
                cpf: cpf,
                nomeCompleto: nomeCompleto,
                dataNascimento: dataNascimento,
                email: email,
                telefone: null!
            );

            // Assert
            act.Should().Throw<ArgumentNullException>()
                .WithMessage("O telefone não pode ser nulo. (Parameter 'telefone')");
        }

        [Fact]
        public void DeveGerarClienteSemIdInicial()
        {
            // Arrange
            var cpf = new Cpf("12345678909");
            var nomeCompleto = "João da Silva Santos";
            var dataNascimento = new DateTime(1990, 3, 15);
            var email = new Email("joao.silva@email.com");
            var telefone = new Telefone("11987654321");

            // Act
            var cliente = new Cliente(
                cpf: cpf,
                nomeCompleto: nomeCompleto,
                dataNascimento: dataNascimento,
                email: email,
                telefone: telefone
            );

            // Assert
            cliente.Id.Should().Be(0); // ID é 0 até ser persistido
        }
    }
}