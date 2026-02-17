using System.Text.RegularExpressions;

namespace Clientes.Domain.ValueObjects;

/// <summary>
/// Value Object para representar Email no domínio.
/// </summary>
public sealed class Email : IEquatable<Email>
{
    public string Valor { get; }

    public Email(string valor)
    {
        if (valor is null)
            throw new ArgumentNullException(nameof(valor), "O email não pode ser nulo.");

        if (string.IsNullOrWhiteSpace(valor))
            throw new ArgumentException("O email não pode ser vazio.", nameof(valor));

        var emailLimpo = valor.Trim().ToLower();

        if (!ValidarFormato(emailLimpo))
            throw new ArgumentException("Email inválido: formato incorreto.", nameof(valor));

        Valor = emailLimpo;
    }

    private static bool ValidarFormato(string email)
    {
        // Regex simples para validar formato básico de email
        var regex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.IgnoreCase);
        return regex.IsMatch(email);
    }

    #region Igualdade por Valor

        public override bool Equals(object? obj) => Equals(obj as Email);

    public bool Equals(Email? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return Valor == other.Valor;
    }

    public override int GetHashCode() => Valor.GetHashCode();

    public static bool operator ==(Email? left, Email? right)
    {
        if (left is null) return right is null;
        return left.Equals(right);
    }

    public static bool operator !=(Email? left, Email? right)
    {
        return !(left == right);
    }

    #endregion

    public override string ToString() => Valor;
}