using FluentAssertions;
using JobTrackerDataManager.data;
using JobTrackerDataManager.Endpoints;
using JobTrackerDataManager.Models;
using Microsoft.AspNetCore.Identity;
using NSubstitute;
using Xunit;

namespace JobTrackerDataManager.Tests;

public class UserEndpointsTests
{
    private readonly IUserStore<ApplicationUser> store = Substitute.For<IUserStore<ApplicationUser>>();

    [Fact]
    public async void GetToken_ReturnsToken_WhenUserDataIsValid()
    {
        var userManager =
            Substitute.For<UserManager<ApplicationUser>>(store, null, null, null, null, null, null, null, null);

        userManager.FindByEmailAsync(Arg.Any<string>()).Returns((ApplicationUser?) null);
        userManager.CreateAsync(Arg.Any<ApplicationUser>(), Arg.Any<string>()).Returns(IdentityResult.Success);

        // Act
        var result = await UserEndpoints.RegisterUser(new UserRegistrationModel(), userManager);

        // Assert
        result.GetOkObjectResultStatusCode().Should().Be(200);
    }

    [Fact]
    public async void GetToken_ReturnsConflict409_WhenUserIsAlreadyRegistered()
    {
        var userManager =
            Substitute.For<UserManager<ApplicationUser>>(store, null, null, null, null, null, null, null, null);

        var identityUser = new ApplicationUser()
        {
            Email = "testuser@gmail.com",
            UserName = "TestUser"
        };

        userManager.FindByEmailAsync(Arg.Any<string>()).Returns(identityUser);
        userManager.CreateAsync(Arg.Any<ApplicationUser>(), Arg.Any<string>()).Returns(IdentityResult.Success);

        // Act
        var result = await UserEndpoints.RegisterUser(new UserRegistrationModel(), userManager);

        // Assert
        result.GetConflictObjectResultStatusCode().Should().Be(409);
    }
}