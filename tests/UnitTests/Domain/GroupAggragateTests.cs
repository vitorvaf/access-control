using AccessControl.Domain.Aggregates.GroupAggragate;
using AccessControl.Domain.Entities;
using AccessControl.Domain.Exceptions;

namespace UnitTests.Domain;

[TestFixture]
public class GroupAggragateTests
{

    private Group _group;
    private Module _module;
    private Permission _permission;

    [SetUp]
    public void Setup()
    {
        _group = new Group("Developers");
        _module = new Module("Administrativo", "Descrição do módulo");
        _permission = new Permission("Leitura", "Descrição da permissão de leitura");
    }

    [Test]
    public void AddModule_AddsModuleToGroup()
    {
        _group.AddModule(_module);

        Assert.That(_group.Modules.Any(m => m.Module == _module), Is.True);
    }

    [Test]
    public void RemoveModule_RemovesModuleFromGroup()
    {
        _group.AddModule(_module);
        _group.RemoveModule(_module);

        Assert.That(_group.Modules.Any(m => m.Module == _module), Is.False);
    }

     [Test]
    public void AddModule_ShouldAddModule_WhenModuleIsNotAlreadyInGroup()
    {
        // Act
        _group.AddModule(_module);

        // Assert
        Assert.IsTrue(_group.Modules.Any(m => m.Module == _module));
    }

    [Test]
    public void RemoveModule_ShouldRemoveModule_WhenModuleIsInGroup()
    {
        // Arrange
        _group.AddModule(_module);

        // Act
        _group.RemoveModule(_module);

        // Assert
        Assert.IsFalse(_group.Modules.Any(m => m.Module == _module));
    }

    [Test]
    public void AddPermission_ShouldAddPermission_WhenPermissionIsNotAlreadyInGroup()
    {
        // Act
        _group.AddPermission(_permission);

        // Assert
        Assert.IsTrue(_group.Permissions.Any(p => p.Permission == _permission));
    }

    [Test]
    public void RemovePermission_ShouldRemovePermission_WhenPermissionIsInGroup()
    {
        // Arrange
        _group.AddPermission(_permission);

        // Act
        _group.RemovePermission(_permission);

        // Assert
        Assert.IsFalse(_group.Permissions.Any(p => p.Permission == _permission));
    }

    [Test]
    public void AddModule_ShouldAddModule_WhenModuleIsNotInGroup()
    {
        // Act
        _group.AddModule(_module);

        // Assert
        Assert.IsTrue(_group.Modules.Any(m => m.Module == _module));
    }

    [Test]
    public void AddPermission_ShouldAddPermission_WhenPermissionIsNotInGroup()
    {
        // Act
        _group.AddPermission(_permission);

        // Assert
        Assert.IsTrue(_group.Permissions.Any(p => p.Permission == _permission));
    }

    // Testes Negativos

    [Test]
    public void AddModule_ShouldThrowException_WhenModuleIsAlreadyInGroup()
    {
        // Arrange
        _group.AddModule(_module);

        // Act & Assert
        Assert.Throws<DomainException>(() => _group.AddModule(_module));
    }

    [Test]
    public void RemoveModule_ShouldThrowException_WhenModuleIsNotInGroup()
    {
        // Act & Assert
        Assert.Throws<DomainException>(() => _group.RemoveModule(_module));
    }

    [Test]
    public void AddPermission_ShouldThrowException_WhenPermissionIsAlreadyInGroup()
    {
        // Arrange
        _group.AddPermission(_permission);

        // Act & Assert
        Assert.Throws<DomainException>(() => _group.AddPermission(_permission));
    }

    [Test]
    public void RemovePermission_ShouldThrowException_WhenPermissionIsNotInGroup()
    {
        // Act & Assert
        Assert.Throws<DomainException>(() => _group.RemovePermission(_permission));
    }
    

}
