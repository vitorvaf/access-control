namespace AccessControl.Domain.Entities;

public class Permission
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }

    public Permission(string name, string description)
    {
        // Validações para a permissão
        Name = name;
        Description = description;
    }    
}
