namespace AccessControl.Api;

public record UserUpdatedDto (
    string name,
    string email,
    string oldPassword,
    string newPassword,
    string role,
    string group
);
