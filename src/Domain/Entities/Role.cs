namespace AccessControl.Domain.Entities;

public class Role
{
    public int Id { get; private set; }
    public string Name { get; private set; }

    public Role(string name)
    {
        // Validações para o nome do papel
        Name = name;
    }

    // Métodos relacionados ao papel
}
