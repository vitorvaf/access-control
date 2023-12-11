namespace AccessControl.Api;

public record UserCreateDto ( 
    string Name,
    string Email,
    string Password,
    int RoleId,
    int GroupId
);
