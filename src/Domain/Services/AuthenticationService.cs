using System.Security.Authentication;
using AccessControl.Domain.Aggregates.UserAggregate;
using AccessControl.Domain.Repositories;
using AccessControl.Domain.ValueObjects;

namespace AccessControl.Domain.Services;

public class AuthenticationService
{
    private readonly IUserRepository _userRepository;
    // Incluir dependências para hashing de senha, geração de token, etc.

    public AuthenticationService(IUserRepository userRepository /*, outras dependências */)
    {
        _userRepository = userRepository;
        // Inicializar outras dependências
    }

    public async Task<Token> AuthenticateAsync(string username, string password, CancellationToken cancellationToken)
    {
        // 1. Verificar se o usuário existe
        var user = await _userRepository.SelectAsync(x => x.Username == username, cancellationToken);

        if (user == null)
        {
            throw new AuthenticationException("Usuário não encontrado.");
        }

        // 2. Verificar se a senha está correta
        if (!VerifyPassword(password, user.Password.Hash))
        {
            throw new AuthenticationException("Senha incorreta.");
        }

        // 3. Gerar token JWT
        var token = GenerateJwtToken(user);

        return token;
    }

    private bool VerifyPassword(string providedPassword, string storedHash)
    {
        // Implementar a lógica de verificação de senha
        return true; // Exemplo
    }

    private Token GenerateJwtToken(User user)
    {
        // Implementar a lógica de geração de token JWT
        var accessToken = "example_token"; // Exemplo de token gerado
        var expiry = DateTime.UtcNow.AddHours(1); // Exemplo de data de expiração

        return new Token(accessToken, expiry);
    }
    
}

