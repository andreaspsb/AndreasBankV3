using Clientes.Domain.ValueObjects;

namespace Clientes.Domain.Entities
{
    public class Cliente
    {

        public Cliente(Cpf cpf, string nomeCompleto, DateTime dataNascimento, Email email, Telefone telefone)
        {
            // Validações de Value Objects
            if (cpf is null)
                throw new ArgumentNullException(nameof(cpf), "O CPF não pode ser nulo.");

            if (email is null)
                throw new ArgumentNullException(nameof(email), "O e-mail não pode ser nulo.");

            if (telefone is null)
                throw new ArgumentNullException(nameof(telefone), "O telefone não pode ser nulo.");

            // Validação de nome completo
            if (nomeCompleto is null)
                throw new ArgumentNullException(nameof(nomeCompleto), "O nome completo não pode ser nulo.");

            if (string.IsNullOrWhiteSpace(nomeCompleto))
                throw new ArgumentException("O nome completo não pode ser vazio.", nameof(nomeCompleto));

            // Validação de idade mínima (RN11)
            var idade = CalcularIdade(dataNascimento);
            if (idade < 16)
                throw new ArgumentException("Cliente deve ter no mínimo 16 anos.");

            Cpf = cpf;
            NomeCompleto = nomeCompleto;
            DataNascimento = dataNascimento;
            Email = email;
            Telefone = telefone;
            Status = "Ativo";
            DataCadastro = DateTime.Now;
        }

        private int CalcularIdade(DateTime dataNascimento)
        {
            var hoje = DateTime.Today;
            var idade = hoje.Year - dataNascimento.Year;
            if (dataNascimento.Date > hoje.AddYears(-idade))
                idade--;
            return idade;
        }

        public long Id { get; private set; }
        public Cpf Cpf { get; private set; }
        public string NomeCompleto { get; private set; }
        public DateTime DataNascimento { get; private set; }
        public Email Email { get; private set; }
        public Telefone Telefone { get; private set; }
        public string Status { get; private set; }
        public DateTime DataCadastro { get; private set; }
    }
}