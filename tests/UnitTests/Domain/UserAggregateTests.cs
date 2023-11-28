using NUnit.Framework;
using System.Collections.Generic;
using AccessControl.Domain.Aggregates.UserAggregate;
using AccessControl.Domain.Aggregates.GroupAggragate;
using AccessControl.Domain.Entities;
using AccessControl.Domain.Exceptions;

namespace UnitTests.Domain;

[TestFixture]
public class UserAggregaterTests
{
    private User _user;
    private Group _group;
    private List<UserGroup> _userGroups;
    private Role _adminRole;
    private Group _devGroup;

    [SetUp]
    public void Setup()
    {
        _user = new User("user123", "teste@email.com", string.Empty); // Exemplo de inicialização
        _adminRole = new Role("Admin");
        _devGroup = new Group("Developers");        
        _group = new Group("Group1");
        
        _user.JoinGroup(_group);

        _userGroups = new List<UserGroup>
        {
           new(_user, _group)
        };

    }

    [Test]
    public void Groups_ReturnsReadOnlyCollectionOfUserGroups()
    {
        var groups = _user.Groups.Select(x => x.Group).ToList();
    
        Assert.That(groups, Is.EquivalentTo(_userGroups.Select(x => x.Group).ToList()));
        Assert.That(_user.Groups, Is.InstanceOf<IReadOnlyCollection<UserGroup>>());
    }

    [Test]
    public void AddGroup_AddsGroupToUser()
    {
        var newGroup = new Group("Group3");
        var group = new UserGroup(_user, newGroup);

        _user.JoinGroup(newGroup);

        Assert.That(_user.Groups.Select(x => x.Group), Contains.Item(group.Group));
    }

    [Test]
    public void RemoveGroup_RemovesGroupFromUser()
    {
        var group = _user.Groups.First();

        _user.LeaveGroup(group.Group);

        Assert.That(_user.Groups, Does.Not.Contain(group));
    }

    [Test]
    public void JoinGroup_WhenUserIsAlreadyInGroup_DoesNotAddGroup()
    {
        var group = _user.Groups.First().Group;        

        // Act & Assert
        Assert.Throws<DomainException>(() => _user.JoinGroup(group));
        Assert.That(_user.Groups.Count(g => g.Group == group), Is.EqualTo(1));
    }

    [Test]
    public void LeaveGroup_WhenUserIsNotInGroup_DoesNotRemoveGroup()
    {
        var group = new Group("Group4");
                
        // Act & Assert
        Assert.Throws<DomainException>(() => _user.LeaveGroup(group));
        Assert.That(_user.Groups, Does.Not.Contain(new UserGroup(_user, group)));
    }

    [Test]
    public void LeaveGroup_WhenUserIsInMultipleGroups_RemovesCorrectGroup()
    {
        var group1 = _user.Groups.First().Group;
        var group2 = new Group("Group5");
        _user.JoinGroup(group2);

        _user.LeaveGroup(group1);

        Assert.That(_user.Groups.Select(x=> x.Group), Does.Not.Contain(group1));
        Assert.That(_user.Groups.Select(x=> x.Group), Contains.Item(group2));
    }

     [Test]
    public void AddRole_ShouldAddRole_WhenRoleIsNotPresent()
    {
        // Act
        _user.AddRole(_adminRole);

        // Assert
        Assert.Contains(_adminRole, _user.Roles.Select(x => x.Role).ToList());
    }

    [Test]
    public void AddRole_ShouldThrowException_WhenRoleIsAlreadyPresent()
    {
        // Arrange
        _user.AddRole(_adminRole);

        // Act & Assert
        Assert.Throws<DomainException>(() => _user.AddRole(_adminRole));
    }

    [Test]
    public void RemoveRole_ShouldRemoveRole_WhenRoleIsPresent()
    {
        // Arrange
        _user.AddRole(_adminRole);

        // Act
        _user.RemoveRole(_adminRole);

        // Assert
        Assert.IsFalse(_user.Roles.Select(x => x.Role).ToList().Contains(_adminRole));
    }

    [Test]
    public void RemoveRole_ShouldThrowException_WhenRoleIsNotPresent()
    {
        // Act & Assert
        Assert.Throws<DomainException>(() => _user.RemoveRole(_adminRole));
    }

    [Test]
    public void JoinGroup_ShouldAddGroup_WhenGroupIsNotPresent()
    {
        // Act
        _user.JoinGroup(_devGroup);

        // Assert
        Assert.Contains(_devGroup, _user.Groups.Select(x => x.Group).ToList());
    }

    [Test]
    public void JoinGroup_ShouldThrowException_WhenGroupIsAlreadyPresent()
    {
        // Arrange
        _user.JoinGroup(_devGroup);

        // Act & Assert
        Assert.Throws<DomainException>(() => _user.JoinGroup(_devGroup));
    }

    [Test]
    public void LeaveGroup_ShouldRemoveGroup_WhenGroupIsPresent()
    {
        // Arrange
        _user.JoinGroup(_devGroup);

        // Act
        _user.LeaveGroup(_devGroup);

        // Assert
        Assert.IsFalse(_user.Groups.Select(x => x.Group).ToList().Contains(_devGroup));
    }

    [Test]
    public void LeaveGroup_ShouldThrowException_WhenGroupIsNotPresent()
    {
        // Act & Assert
        Assert.Throws<DomainException>(() => _user.LeaveGroup(_devGroup));
    }
}