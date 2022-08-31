using JobTrackerDataManager.data;
using JobTrackerDataManager.Models;
using Microsoft.AspNetCore.Identity;
using Serilog;

namespace JobTrackerDataManager.Endpoints;

public static class UserEndpoints
{
    public static void ConfigureUserEndpoints(this WebApplication app)
    {
        app.MapPost("/user/register", RegisterUser)
            .Accepts<UserRegistrationModel>("application/json");
    }

    internal static async Task<IResult> RegisterUser(UserRegistrationModel user,
        UserManager<ApplicationUser> userManager)
    {
        var existingUser = await userManager.FindByEmailAsync(user.EmailAddress);

        if (existingUser is not null)
        {
            return Results.Conflict();
        }

        ApplicationUser newUser = new()
        {
            Email = user.EmailAddress,
            EmailConfirmed = true,
            UserName = user.EmailAddress
        };

        try
        {
            IdentityResult result = await userManager.CreateAsync(newUser, user.Password);

            if (result.Succeeded)
            {
                return Results.Ok();
            }
        }
        catch (Exception ex)
        {
            Log.Error("Unable to create User");
        }

        return Results.Problem("Unable to create user.", null, 500);
    }
}