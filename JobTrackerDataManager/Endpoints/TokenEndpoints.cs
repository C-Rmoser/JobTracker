using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using JobTrackerDataManager.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace JobTrackerDataManager.Endpoints;

public static class TokenEndpoints
{
    public static void ConfigureTokenEndpoints(this WebApplication app)
    {
        app.MapPost("/Token", GetToken)
            .Accepts<UserLoginModel>("application/json")
            .Produces<string>()
            .AllowAnonymous();
    }

    internal static async Task<IResult> GetToken(UserLoginModel user, IConfiguration config,
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
            return Results.Unauthorized();
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

        return Results.Ok(new {token = tokenString});
    }
}