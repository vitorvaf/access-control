namespace AccessControl.Api;

public static class UserAggragateExtension
{
    public static void MapUserEndpoints(this WebApplication app)
    {
        app.MapGet("/users", (IUserService service, Guid userId) => service.GetUserAsync(userId));
        app.MapGet("/users/{id}", (IUserService service, Guid id) => service.GetUserAsync(id));
        app.MapPost("/users", (IUserService service, UserCreateDto newUser) => service.CreateUserAsync(newUser));
        app.MapPut("/users/{id}", (IUserService service, Guid id, UserUpdatedDto user) => service.UpdateUserAsync(id, user));
        app.MapDelete("/users/{id}", (IUserService service, Guid id) => service.DeleteUserAsync(id));
    }
}
