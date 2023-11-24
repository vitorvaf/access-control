namespace AccessControl.Domain.ValueObjects;
public class Token
{
    public string AccessToken { get; private set; }
    public DateTime Expiry { get; private set; }
    // Outras propriedades relevantes, como RefreshToken, TokenType, etc.

    public Token(string accessToken, DateTime expiry /*, outros parâmetros */)
    {
        AccessToken = accessToken;
        Expiry = expiry;
        // Inicializar outras propriedades
    }

    // Verifica se o token está expirado
    public bool IsExpired() => DateTime.UtcNow > Expiry;

    // Pode ser usado para renovar o token, se aplicável
    public Token Renew(string newAccessToken, DateTime newExpiry)
    {
        return new Token(newAccessToken, newExpiry);
    }

    // Método para validar o token (pode ser expandido com lógica adicional)
    public bool IsValid()
    {
        // Implementar lógica de validação, como verificar a assinatura do token
        return !IsExpired(); // Exemplo básico
    }
    
}
