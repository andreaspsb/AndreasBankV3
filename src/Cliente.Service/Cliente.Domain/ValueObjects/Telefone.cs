using System.Text.RegularExpressions;

namespace Clientes.Domain.ValueObjects;

/// <summary>
/// Value Object para representar Telefone no domínio.
/// </summary>
public sealed class Telefone : IEquatable<Telefone>
{
    public string Valor { get; }

    public Telefone(string valor)
    {
        if (valor is null)
            throw new ArgumentNullException(nameof(valor), "O telefone não pode ser nulo.");

        if (string.IsNullOrWhiteSpace(valor))
            throw new ArgumentException("O telefone não pode ser vazio.", nameof(valor));

        var telefoneLimpo = valor.Trim();

        if (!ValidarApenasDigitos(telefoneLimpo))
            throw new ArgumentException("Telefone inválido: deve conter apenas dígitos.", nameof(valor));

        if (!ValidarTamanho(telefoneLimpo))
            throw new ArgumentException("Telefone inválido: deve conter entre 10 e 11 dígitos.", nameof(valor));

        Valor = telefoneLimpo;
    }

    private static bool ValidarApenasDigitos(string telefone)
    {
        return Regex.IsMatch(telefone, @"^\d+$");
    }

    private static bool ValidarTamanho(string telefone)
    {
        return telefone.Length >= 10 && telefone.Length <= 11;
    }

    #region Igualdade por Valor

    public override bool Equals(object? obj) => Equals(obj as Telefone);

    public bool Equals(Telefone? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return Valor == other.Valor;
    }

    public override int GetHashCode() => Valor.GetHashCode();

    public static bool operator ==(Telefone? left, Telefone? right)
    {
        if (left is null) return right is null;
        return left.Equals(right);
    }

    public static bool operator !=(Telefone? left, Telefone? right)
    {
        return !(left == right);
    }

    #endregion

    public override string ToString() => Valor;
}