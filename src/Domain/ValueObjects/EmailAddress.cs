namespace AccessControl.Domain.ValueObjects;

public class EmailAddress
{
    public string Value { get; private set; }

    public EmailAddress(string value)
    {
        // Validação do e-mail
        Value = value;
    }

    // Sobrescreva Equals, GetHashCode e ToString
}
