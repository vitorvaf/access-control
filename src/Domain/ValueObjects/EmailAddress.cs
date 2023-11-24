using System.Text.RegularExpressions;
using AccessControl.Domain.Exceptions;
using AccessControl.Domain.Extensions;
using AccessControl.Domain.SeedWork;

namespace AccessControl.Domain.ValueObjects;

public partial class EmailAddress : ValueObject
{
    private const string Pattern = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";

    protected EmailAddress()
    {
    }

    public EmailAddress(string address)
    {
        if (string.IsNullOrEmpty(address))
            throw new DomainException("E-mail inválido");

        Address = address.Trim().ToLower();

        if (Address.Length < 5)
            throw new DomainException("E-mail inválido");

        if (!EmailRegex().IsMatch(Address))
            throw new DomainException("E-mail inválido");
    }

    public string Address { get; }
    public string Hash => Address.ToBase64();
    public Verification Verification { get; private set; } = new();

    public void ResendVerification()
        => Verification = new Verification();

    public static implicit operator string(EmailAddress email)
        => email.ToString();

    public static implicit operator EmailAddress(string address)
        => new(address);

    public override string ToString()
        => Address;

    [GeneratedRegex(Pattern)]
    private static partial Regex EmailRegex();
}