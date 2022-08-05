using FluentAssertions;
using JobTrackerDataManager.Endpoints;
using JobTrackerDataManager.Models;
using Microsoft.AspNetCore.Identity;
using NSubstitute;
using Xunit;

namespace JobTrackerDataManager.Tests;

public class UserEndpointsTests
{
    private readonly IUserStore<IdentityUser> store = Substitute.For<IUserStore<IdentityUser>>();

    [Fact]
    public async void GetToken_ReturnsToken_WhenUserDataIsValid()
    {
        var userManager =
            Substitute.For<UserManager<IdentityUser>>(store, null, null, null, null, null, null, null, null);

        userManager.FindByEmailAsync(Arg.Any<string>()).Returns((IdentityUser?) null);
        userManager.CreateAsync(Arg.Any<IdentityUser>(), Arg.Any<string>()).Returns(IdentityResult.Success);

        // Act
        // var result = await JobEndpoints.GetJobs(_data);
        var result = await UserEndpoints.RegisterUser(new UserRegistrationModel(), userManager);

        // Assert
        result.GetOkObjectResultStatusCode().Should().Be(200);
    }

    [Fact]
    public async void GetToken_ReturnsConflict409_WhenUserIsAlreadyRegistered()
    {
        var userManager =
            Substitute.For<UserManager<IdentityUser>>(store, null, null, null, null, null, null, null, null);

        var identityUser = new IdentityUser()
        {
            Email = "testuser@gmail.com",
            UserName = "TestUser"
        };

        userManager.FindByEmailAsync(Arg.Any<string>()).Returns(identityUser);
        userManager.CreateAsync(Arg.Any<IdentityUser>(), Arg.Any<string>()).Returns(IdentityResult.Success);

        // Act
        // var result = await JobEndpoints.GetJobs(_data);
        var result = await UserEndpoints.RegisterUser(new UserRegistrationModel(), userManager);

        // Assert
        result.GetConflictObjectResultStatusCode().Should().Be(409);
    }
}