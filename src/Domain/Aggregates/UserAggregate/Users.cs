using AccessControl.Domain.Aggregates.GroupAggragate;
using AccessControl.Domain.Entities;
using AccessControl.Domain.SeedWork;
using AccessControl.Domain.ValueObjects;

namespace AccessControl.Domain.Aggregates.UserAggregate;

public class User : Entity
{
    public string Username { get; private set; }
    public EmailAddress Email { get; private set; } // Utilizando o objeto de valor EmailAddress
    public Password Password { get; private set; } = null!;
    

    private readonly List<UserRole> _roles = new List<UserRole>();
    public IReadOnlyCollection<UserRole> Roles => _roles.AsReadOnly();

    private readonly List<UserGroup> _groups = new List<UserGroup>();
    public IReadOnlyCollection<UserGroup> Groups => _groups.AsReadOnly();

    public User(string username, EmailAddress email, string password)
    {
        Username = username;
        Email = email;
        SetPassword(password);
    }

    public void SetPassword(string plainTextPassword)
    {
        var password = new Password(plainTextPassword);
        Password = password;
        
    }
    public void UpdatePassword(string plainTextPassword, string code)
    {
        if (!string.Equals(code.Trim(), Password.ResetCode.Trim(), StringComparison.CurrentCultureIgnoreCase))
            throw new Exception("Código de restauração inválido");

        var password = new Password(plainTextPassword);
        Password = password;
    }
    public void AddRole(Role role)
    {
        var userRole = new UserRole(this, role);
        _roles.Add(userRole);
    }

    public void RemoveRole(Role role)
    {
        var userRole = _roles.FirstOrDefault(r => r.Role == role);
        if (userRole != null)
        {
            _roles.Remove(userRole);
        }
    }

    public void JoinGroup(Group group)
    {
        var userGroup = new UserGroup(this, group);
        _groups.Add(userGroup);
    }

    public void LeaveGroup(Group group)
    {
        var userGroup = _groups.FirstOrDefault(g => g.Group == group);
        if (userGroup != null)
        {
            _groups.Remove(userGroup);
        }
    }

    // Outros métodos e comportamentos relevantes
}
