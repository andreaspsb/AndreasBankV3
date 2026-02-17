using Clientes.Domain.ValueObjects;

namespace Clientes.Domain.Entities
{
    public class Cliente
    {

        public Cliente(Cpf cpf, string nomeCompleto, DateTime dataNascimento, Email email, string telefone)
        {
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

        public Cpf Cpf { get; private set; }
        public string NomeCompleto { get; private set; }
        public DateTime DataNascimento { get; private set; }
        public Email Email { get; private set; }
        public string Telefone { get; private set; }
        public string Status { get; private set; }
        public DateTime DataCadastro { get; private set; }
    }
}