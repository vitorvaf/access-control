namespace AccessControl.Api;

public static class GroupAggragateExtension
{

    public static void MapGroupEndpoints(this WebApplication app)
    {
        // app.MapGet("/groups", (IGroupService service) => service.GetGroupsAsync());
        // app.MapGet("/groups/{id}", (IGroupService service, int id) => service.GetGroupAsync(id));
        // app.MapPost("/groups", (IGroupService service, Group group) => service.CreateGroupAsync(group));
        // app.MapPut("/groups/{id}", (IGroupService service, int id, Group group) => service.UpdateGroupAsync(id, group));
        // app.MapDelete("/groups/{id}", (IGroupService service, int id) => service.DeleteGroupAsync(id));
    }

}
