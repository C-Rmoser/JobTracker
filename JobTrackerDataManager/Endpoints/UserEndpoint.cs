using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DataAccess.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace JobTrackerDataManager.Endpoints;

public static class UserEndpoint
{
    public static void ConfigureUserEndpoint(this WebApplication app)
    {
        app.MapPost("/User/Login", Login)
            .Accepts<UserLoginModel>("application/json")
            .Produces<string>()
            .AllowAnonymous();
        app.MapPost("/User/Register", RegisterUser)
            .Accepts<UserRegistrationModel>("application/json");
    }

    private static async Task<IResult> Login(UserLoginModel user, IConfiguration config,
        UserManager<IdentityUser> userManager)
    {
        if (string.IsNullOrEmpty(user.EmailAddress) || string.IsNullOrEmpty(user.Password))
        {
            return Results.BadRequest("Invalid user credentials");
        }

        var loggedInUser = await userManager.FindByEmailAsync(user.EmailAddress);


        if (loggedInUser is null)
        {
            return Results.NotFound("User not found");
        }

        if (!await userManager.CheckPasswordAsync(loggedInUser, user.Password))
        {
            return Results.Problem("Password is incorrect.");
        }

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, loggedInUser.UserName),
            new Claim(ClaimTypes.Email, loggedInUser.Email),
        };

        var token = new JwtSecurityToken
        (
            issuer: config["Jwt:Issuer"],
            audience: config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddDays(60),
            notBefore: DateTime.UtcNow,
            signingCredentials: new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"])),
                SecurityAlgorithms.HmacSha256)
        );

        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

        return Results.Ok(tokenString);
    }

    private record UserRegistrationModel(
        string EmailAddress,
        string Password
    );

    private static async Task<IResult> RegisterUser(UserRegistrationModel user, UserManager<IdentityUser> userManager)
    {
        var existingUser = await userManager.FindByEmailAsync(user.EmailAddress);

        if (existingUser is not null)
        {
            return Results.Problem($"User with Email: {user.EmailAddress} is already registered");
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