using AccessControl.Domain.Aggregates.GroupAggragate;

namespace AccessControl.Api;

public interface IGroupService
{
        Task<Group> CreateGroupAsync(Group group);
        Task<Group> UpdateGroupAsync(Guid groupId, Group updatedGroup);
        Task<Group> GetGroupAsync(Guid groupId);
        Task<IEnumerable<Group>> GetAllGroupsAsync();
        Task DeleteGroupAsync(Guid groupId);
        Task AddUserToGroupAsync(Guid userId, Guid groupId);
        Task RemoveUserFromGroupAsync(Guid userId, Guid groupId);

}
