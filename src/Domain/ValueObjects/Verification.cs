﻿using AccessControl.Domain.Exceptions;
using AccessControl.Domain.SeedWork;

namespace AccessControl.Domain.ValueObjects;

public class Verification : ValueObject
{
    public Verification()
    {
    }

    public string Code { get; } = Guid.NewGuid().ToString("N")[..6].ToUpper();
    public DateTime? ExpiresAt { get; private set; } = DateTime.UtcNow.AddMinutes(5);
    public DateTime? VerifiedAt { get; private set; } = null;
    public bool IsActive => VerifiedAt != null && ExpiresAt == null;

    public void Verify(string code)
    {
        if (IsActive)
            throw new DomainException("Este item já foi ativado");

        if (ExpiresAt < DateTime.UtcNow)
            throw new DomainException("Este código já expirou");

        if (!string.Equals(code.Trim(), Code.Trim(), StringComparison.CurrentCultureIgnoreCase))
            throw new DomainException("Código de verificação inválido");

        ExpiresAt = null;
        VerifiedAt = DateTime.UtcNow;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Code;
        yield return ExpiresAt;
        yield return VerifiedAt;
        yield return IsActive;
    }
}
