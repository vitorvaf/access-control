namespace AccessControl.Api;

public static class UserAggragateExtension
{    
        public static void MapUserEndpoints(this WebApplication app)
        {
            // app.MapGet("/users", (IUserService service) => service.GetUsersAsync());
            // app.MapGet("/users/{id}", (IUserService service, int id) => service.GetUserAsync(id));
            // app.MapPost("/users", (IUserService service, User user) => service.CreateUserAsync(user));
            // app.MapPut("/users/{id}", (IUserService service, int id, User user) => service.UpdateUserAsync(id, user));
            // app.MapDelete("/users/{id}", (IUserService service, int id) => service.DeleteUserAsync(id));
        }
}
