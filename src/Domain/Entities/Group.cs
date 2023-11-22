namespace AccessControl.Domain.Entities;

public class Group
{
    public int Id { get; private set; }
    public string Name { get; private set; }

    public Group(string name)
    {
        // Validações para o nome do grupo
        Name = name;
    }

    // Métodos relacionados ao grupo
}
