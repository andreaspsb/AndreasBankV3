namespace Clientes.Domain.ValueObjects;

/// <summary>
///  Value Object para representar o CPF (Cadastro de Pessoas Físicas) no domínio de autenticação.
/// 
public sealed class Cpf : IEquatable<Cpf>
{
    public string Valor { get; }

    public Cpf(string valor)
    {
        if (valor is null)
            throw new ArgumentNullException(nameof(valor), "CPF não pode ser nulo.");

        if (string.IsNullOrWhiteSpace(valor))
            throw new ArgumentException("CPF não pode ser vazio.", nameof(valor));

        var cpfLimpo = LimparCpf(valor);

        ValidarTamanho(cpfLimpo);
        ValidarDigitosRepetidos(cpfLimpo);
        ValidarDigitosVerificadores(cpfLimpo);

        Valor = cpfLimpo;
    }

    private static string LimparCpf(string cpf)
    {
        // Remove caracteres não numéricos
        return new string(cpf.Where(char.IsDigit).ToArray());
    }

    private static void ValidarTamanho(string cpf)
    {
        if (cpf.Length != 11)
            throw new ArgumentException("CPF inválido: deve conter exatamente 11 dígitos.", nameof(cpf));
    }

    private static void ValidarDigitosRepetidos(string cpf)
    {
        if (cpf.All(c => c == cpf[0]))
            throw new ArgumentException("CPF inválido: não pode conter dígitos repetidos.", nameof(cpf));
    }

    private static void ValidarDigitosVerificadores(string cpf)
    {
        var digitoVerificador1 = CalcularDigitoVerificador(cpf.Substring(0, 9));
        var digitoVerificador2 = CalcularDigitoVerificador(cpf.Substring(0, 10));

        if (cpf[9] != digitoVerificador1 || cpf[10] != digitoVerificador2)
            throw new ArgumentException("CPF inválido: dígitos verificadores incorretos.", nameof(cpf));
    }

    private static char CalcularDigitoVerificador(string cpfParcial)
    {
        var soma = 0;
        var multiplicador = cpfParcial.Length + 1;

        for (int i = 0; i < cpfParcial.Length; i++)
        {
            soma += int.Parse(cpfParcial[i].ToString()) * multiplicador--;
        }

        var resto = soma % 11;
        var digito = resto < 2 ? 0 : 11 - resto;

        return digito.ToString()[0];
    }

    #region Igualdade por Valor


    public override bool Equals(object? obj) => Equals(obj as Cpf);

    public bool Equals(Cpf? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return Valor == other.Valor;
    }
    public override int GetHashCode() => Valor.GetHashCode();

    public static bool operator ==(Cpf? left, Cpf? right)
    {
        if (left is null) return right is null;
        return left.Equals(right);
    }

    public static bool operator !=(Cpf? left, Cpf? right)
    {
        return !(left == right);
    }

    #endregion

    public override string ToString() => Valor;

}