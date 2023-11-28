using System.IdentityModel.Tokens.Jwt;
using System.Security.Authentication;
using System.Security.Claims;
using System.Text;
using AccessControl.Domain.Aggregates.UserAggregate;
using AccessControl.Domain.Repositories;
using AccessControl.Domain.ValueObjects;
using Microsoft.IdentityModel.Tokens;

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
        if (!VerifyPassword(user, password))
        {
            throw new AuthenticationException("Usuário ou senha inválidos.");
        }

        // 3. Verifica se a conta esta ativada    
        if (!user.Email.Verification.IsActive)
               throw new AuthenticationException("Conta inativa.");

        // 3. Gerar token JWT
        var token = GenerateJwtToken(user);

        return token;
    }

    private bool VerifyPassword(User user, string password)
    {
        if (!user.Password.Challenge(password))
            return false;
        
        return true;
    }

    private Token GenerateJwtToken(User user)
    {
        var handler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(Configuration.Secrets.JwtPrivateKey);
        var credentials = new SigningCredentials(
            new SymmetricSecurityKey(key),
            SecurityAlgorithms.HmacSha256Signature);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = GenerateClaims(user),
            Expires = DateTime.UtcNow.AddHours(8),
            SigningCredentials = credentials,
        };
        var token = handler.CreateToken(tokenDescriptor);         

        return new Token(handler.WriteToken(token), DateTime.UtcNow.AddHours(8));
    }

    private static ClaimsIdentity GenerateClaims(User user)
    {
        var ci = new ClaimsIdentity();
        ci.AddClaim(new Claim("id", user.Id.ToString()));
        ci.AddClaim(new Claim(ClaimTypes.GivenName, user.Username));
        ci.AddClaim(new Claim(ClaimTypes.Name, user.Email));
        foreach (var role in user.Roles)
            ci.AddClaim(new Claim(ClaimTypes.Role, role.Role.Name));

        foreach (var group in user.Groups)
            ci.AddClaim(new Claim("Group", group.Group.Name));

        foreach (var group in user.Groups)
            foreach (var module in group.Group.Modules)
                ci.AddClaim(new Claim("module", module.Module.Name));
                
        foreach (var group in user.Groups)
            foreach (var permission in group.Group.Permissions)
                ci.AddClaim(new Claim("permission", permission.Permission.Name));

        return ci;
    }
    
}

