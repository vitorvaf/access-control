using NUnit.Framework;
using Moq;
using AccessControl.Domain.Services;
using AccessControl.Domain.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Security.Authentication;
using AccessControl.Domain.Aggregates.UserAggregate;
using AccessControl.Domain.ValueObjects;
using System.Linq.Expressions;

namespace UnitTests.Domain;


[TestFixture]
public class AuthenticationServiceTests
{
    private Mock<IUserRepository> _userRepositoryMock;
    private AuthenticationService _authenticationService;

    [SetUp]
    public void Setup()
    {
        _userRepositoryMock = new Mock<IUserRepository>();
        _authenticationService = new AuthenticationService(_userRepositoryMock.Object);
    }

    [Test]
    public async Task AuthenticateAsync_ShouldReturnToken_WhenCredentialsAreValid()
    {
        // Arrange
        var validUser = CreateValidUser();
        _userRepositoryMock.Setup(repo => repo.SelectAsync(It.IsAny<Expression<Func<User, bool>>>(), It.IsAny<CancellationToken>()))
                           .ReturnsAsync(validUser);

        // Act
        var result = await _authenticationService.AuthenticateAsync("validUsername", "validPassword", CancellationToken.None);

        // Assert
        Assert.IsNotNull(result);
        Assert.IsInstanceOf<Token>(result);
    }

    [Test]
    public void AuthenticateAsync_ShouldThrowAuthenticationException_WhenUserNotFound()
    {
        // Arrange
        _userRepositoryMock.Setup(repo => repo.SelectAsync(It.IsAny<Expression<Func<User, bool>>>(), It.IsAny<CancellationToken>()))
                           .ReturnsAsync((User)null);

        // Act & Assert
        Assert.ThrowsAsync<AuthenticationException>(() => _authenticationService.AuthenticateAsync("invalidUsername", "password", CancellationToken.None));
    }

    [Test]
    public void AuthenticateAsync_ShouldThrowAuthenticationException_WhenPasswordIsInvalid()
    {
        // Arrange
        var userWithInvalidPassword = CreateValidUser();
        _userRepositoryMock.Setup(repo => repo.SelectAsync(It.IsAny<Expression<Func<User, bool>>>(), It.IsAny<CancellationToken>()))
                           .ReturnsAsync(userWithInvalidPassword);

        // Act & Assert
        Assert.ThrowsAsync<AuthenticationException>(() => _authenticationService.AuthenticateAsync("validUsername", "invalidPassword", CancellationToken.None));
    }

    [Test]
    public void AuthenticateAsync_ShouldThrowAuthenticationException_WhenAccountIsInactive()
    {
        // Arrange
        var inactiveUser = CreateInactiveUser();
        _userRepositoryMock.Setup(repo => repo.SelectAsync(It.IsAny<Expression<Func<User, bool>>>(), It.IsAny<CancellationToken>()))
                           .ReturnsAsync(inactiveUser);

        // Act & Assert
        Assert.ThrowsAsync<AuthenticationException>(() => _authenticationService.AuthenticateAsync("inactiveUsername", "password", CancellationToken.None));
    }

    private User CreateValidUser()
    {
        var validUserId = Guid.NewGuid();
        var validUsername = "validUsername";
        var validPassword = "validPassword"; // Supondo que a senha já esteja devidamente hash e salva
        var validEmail = "valid@example.com";
        var isActive = true;

        var user = new User(validUsername,validEmail,validPassword);        

        return user;
    }

    private User CreateInactiveUser()
    {
        var inactiveUserId = Guid.NewGuid();
        var inactiveUsername = "inactiveUsername";
        var inactivePassword = "password"; // Supondo que a senha já esteja devidamente hash e salva
        var inactiveEmail = "inactive@example.com";
        var isActive = false; // Conta inativa

        var user = new User(inactiveUsername,inactiveEmail,inactivePassword);        

        return user;
    }
  
}
