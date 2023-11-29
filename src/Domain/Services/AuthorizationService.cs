using AccessControl.Domain.Repositories;

namespace AccessControl.Domain;

public class AuthorizationService : IAuthorizationService
{
    private readonly IUserRepository _userRepository;
    // Incluir dependências para verificação de permissões

    public AuthorizationService(IUserRepository userRepository /*, outras dependências */)
    {
        _userRepository = userRepository;
        // Inicializar outras dependências
    }

    public async Task<bool> AuthorizeAsync(Guid userId, string permission, CancellationToken cancellationToken)
    {
        // 1. Obter usuário e seus grupos
        var user = await _userRepository.FindAsync(userId, cancellationToken);
        if (user == null)
        {
            return false;
        }

        // 2. Verificar se o usuário tem a permissão necessária
        return user.Groups
            .Any(g => g.Group.Permissions.Any(p => p.Permission.Name == permission));
    }
    
}
