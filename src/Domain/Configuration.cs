namespace AccessControl.Domain;

public static class Configuration
{
    public static SecretsConfiguration Secrets { get; set; } = new();
    public static EmailConfiguration Email { get; set; } = new();
    public static SendGridConfiguration SendGrid { get; set; } = new();
    public static DatabaseConfiguration Database { get; set; } = new();
    
    public class DatabaseConfiguration
    {
        public string ConnectionString { get; set; } = string.Empty;
    }
    
    public class EmailConfiguration
    {
        public string DefaultFromEmail { get; set; } = "email@email.com";
        public string DefaultFromName { get; set; } = "email.teste";
    }
    
    public class SendGridConfiguration
    {
        public string ApiKey { get; set; } = string.Empty;
    }
    
    public class SecretsConfiguration
    {
        public string ApiKey { get; set; } = "getApiKey";//string.Empty;
        public string JwtPrivateKey { get; set; } = "getJwtPrivateKey43443FDFDF34DF34343fdf344SDFSDFSDFSDFSDF4545354345SDFGDFGDFGDFGdffgfdGDFGDGR%";//string.Empty;
        public string PasswordSaltKey { get; set; } = "getPasswordSaltKey";//string.Empty;
    }
}