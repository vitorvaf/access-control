namespace AccessControl.Domain.Entities;

public class User
{
    public int Id { get; private set; }
    public string Username { get; private set; }
    public string Email { get; private set; }
    private string _password; // Encapsulamento para segurança

    public User(string username, string email, string password)
    {
        // Adicione aqui validações necessárias
        Username = username;
        Email = email;
        SetPassword(password);
    }

    public void SetPassword(string password)
    {
        // Implemente a lógica de hashing e validação de senha
        _password = password;
    }

    // Outros métodos relacionados ao usuário
}
