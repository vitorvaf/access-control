using NUnit.Framework;
using Moq;
using AccessControl.Domain;
using AccessControl.Domain.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;
using AccessControl.Domain.Aggregates.UserAggregate;
using AccessControl.Domain.Entities;
using AccessControl.Domain.Aggregates.GroupAggragate;

namespace UnitTests.Domain;

[TestFixture]
public class AuthorizationServiceTests
{
    private Mock<IUserRepository> _userRepositoryMock;
    private AuthorizationService _authorizationService;
    
    private User _userWithPermission;
    private User _userWithoutPermission;
    private Guid _userIdWithPermission;
    private Guid _userIdWithoutPermission;

    [SetUp]
    public void Setup()
    {
        _userRepositoryMock = new Mock<IUserRepository>();

        // Configurar usuários de teste
        _userIdWithPermission = Guid.NewGuid();
        _userIdWithoutPermission = Guid.NewGuid();
        _userWithPermission = CreateUserWithPermission("read");
        _userWithoutPermission = CreateUserWithoutPermission();

        _userRepositoryMock.Setup(repo => repo.FindAsync(_userIdWithPermission, It.IsAny<CancellationToken>()))
                           .ReturnsAsync(_userWithPermission);
        _userRepositoryMock.Setup(repo => repo.FindAsync(_userIdWithoutPermission, It.IsAny<CancellationToken>()))
                           .ReturnsAsync(_userWithoutPermission);

        _authorizationService = new AuthorizationService(_userRepositoryMock.Object);
        
    }

    [Test]
    public async Task AuthorizeAsync_ShouldReturnTrue_WhenUserHasPermission()
    {
        // Act
        var result = await _authorizationService.AuthorizeAsync(_userIdWithPermission, "read", CancellationToken.None);

        // Assert
        Assert.IsTrue(result);
    }

    [Test]
    public async Task AuthorizeAsync_ShouldReturnFalse_WhenUserDoesNotHavePermission()
    {
        // Act
        var result = await _authorizationService.AuthorizeAsync(_userIdWithoutPermission, "write", CancellationToken.None);

        // Assert
        Assert.IsFalse(result);
    }

    [Test]
    public async Task AuthorizeAsync_ShouldReturnFalse_WhenUserHasNoGroups()
    {
        // Arrange
        var userWithNoGroups = new User("deveoper", "developer@dev.com", "");
        var userIdWithNoGroups = Guid.NewGuid();
        _userRepositoryMock.Setup(repo => repo.FindAsync(userIdWithNoGroups, It.IsAny<CancellationToken>()))
                           .ReturnsAsync(userWithNoGroups);

        // Act
        var result = await _authorizationService.AuthorizeAsync(userIdWithNoGroups, "read", CancellationToken.None);

        // Assert
        Assert.IsFalse(result);
    }

    [Test]
    public async Task AuthorizeAsync_ShouldReturnFalse_WhenGroupsHaveNoPermissions()
    {
        // Arrange
        var userWithGroupsNoPermissions = CreateUserWithGroupsNoPermissions();
        var userIdWithGroupsNoPermissions = Guid.NewGuid();
        _userRepositoryMock.Setup(repo => repo.FindAsync(userIdWithGroupsNoPermissions, It.IsAny<CancellationToken>()))
                           .ReturnsAsync(userWithGroupsNoPermissions);

        // Act
        var result = await _authorizationService.AuthorizeAsync(userIdWithGroupsNoPermissions, "read", CancellationToken.None);

        // Assert
        Assert.IsFalse(result);
    }

    private User CreateUserWithPermission(string permissionName)
    {
        var permission = new Permission(permissionName, "Read permission");
        var group = new Group("Test Group");
        var groupPermission = new GroupPermission(group, permission);
        group.AddPermission(permission);
        var user = new User("deveoper", "developer@dev.com", "");
        user.JoinGroup(group);
        return user;
    }

    private User CreateUserWithoutPermission()
    {
        var group = new Group("Test Group");
        var user = new User("deveoper", "developer@dev.com", "");
        user.JoinGroup(group);
        return user;
    }

    private User CreateUserWithGroupsNoPermissions()
    {
        var user = new User("deveoper", "developer@dev.com", "");

        // Adicionando grupos sem permissões
        var group1 = new Group("Group 1");
        var group2 = new Group("Group 2");        

        user.JoinGroup(group1);
        user.JoinGroup(group2);        

        return user;
    }
}
