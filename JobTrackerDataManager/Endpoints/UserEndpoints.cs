using JobTrackerDataManager.Models;
using Microsoft.AspNetCore.Identity;

namespace JobTrackerDataManager.Endpoints;

public static class UserEndpoints
{
    public static void ConfigureUserEndpoints(this WebApplication app)
    {
        app.MapPost("/User/Register", RegisterUser)
            .Accepts<UserRegistrationModel>("application/json");
    }

    internal static async Task<IResult> RegisterUser(UserRegistrationModel user, UserManager<IdentityUser> userManager)
    {
        var existingUser = await userManager.FindByEmailAsync(user.EmailAddress);

        if (existingUser is not null)
        {
            return Results.Conflict();
        }

        IdentityUser newUser = new()
        {
            Email = user.EmailAddress,
            EmailConfirmed = true,
            UserName = user.EmailAddress
        };

        IdentityResult result = await userManager.CreateAsync(newUser, user.Password);

        if (result.Succeeded)
        {
            return Results.Ok();
        }

        return Results.Problem("Unable to create user.");
    }
}