namespace AccessControl.Domain.Entities;

public class Module
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public int? ParentModuleId { get; private set; }

    public Module(string name, string description, int? parentModuleId = null)
    {
        // Validações para o módulo
        Name = name;
        Description = description;
        ParentModuleId = parentModuleId;
    }

    // Métodos relacionados ao módulo
}
