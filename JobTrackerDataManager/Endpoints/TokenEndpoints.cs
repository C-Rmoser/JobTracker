using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using JobTrackerDataManager.data;
using JobTrackerDataManager.DTOs;
using JobTrackerDataManager.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace JobTrackerDataManager.Endpoints;

public static class TokenEndpoints
{
    public static void ConfigureTokenEndpoints(this WebApplication app)
    {
        app.MapPost("/token", GetToken)
            .Accepts<UserLoginModel>("application/json")
            .AllowAnonymous();

        app.MapPost("/refresh-token", RefreshToken)
            .Accepts<TokenDto>("application/json")
            .AllowAnonymous();
    }

    internal static async Task<IResult> GetToken(UserLoginModel user, IConfiguration config,
        UserManager<ApplicationUser> userManager)
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
            new Claim(ClaimTypes.Name, loggedInUser.UserName),
            new Claim(ClaimTypes.Email, loggedInUser.Email),
        };

        var token = CreateToken(claims.ToList(), config);
        string refreshToken = GenerateRefreshToken();

        _ = int.TryParse(config["Jwt:RefreshTokenValidityInDays"], out int refreshTokenValidityInDays);
        loggedInUser.RefreshToken = refreshToken;
        var refreshTokenExpiresAt = DateTime.Now.AddDays(refreshTokenValidityInDays);
        loggedInUser.RefreshTokenExpiryTime = refreshTokenExpiresAt;

        await userManager.UpdateAsync(loggedInUser);

        return Results.Ok(new
        {
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            TokenExpiresAt = token.ValidTo,
            RefreshToken = refreshToken,
            RefreshTokenExpiresAt = refreshTokenExpiresAt
        });
    }

    private static JwtSecurityToken CreateToken(List<Claim> claims,
        IConfiguration config)
    {
        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
        _ = int.TryParse(config["Jwt:TokenValidityInMinutes"], out int tokenValidityInMinutes);

        var token = new JwtSecurityToken(
            issuer: config["Jwt:Issuer"],
            audience: config["Jwt:Audience"],
            expires: DateTime.Now.AddMinutes(tokenValidityInMinutes),
            notBefore: DateTime.UtcNow,
            claims: claims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
        );

        return token;
    }

    public static async Task<IResult> RefreshToken(TokenDto tokenDto,
        UserManager<ApplicationUser> userManager,
        IConfiguration config)
    {
        string? accessToken = tokenDto.AccessToken;
        string? refreshToken = tokenDto.RefreshToken;

        if (string.IsNullOrEmpty(accessToken) || string.IsNullOrEmpty(refreshToken))
        {
            return Results.BadRequest("Invalid client request");
        }

        var principal = GetPrincipalFromExpiredToken(accessToken, config);
        if (principal == null)
        {
            return Results.BadRequest("Invalid access token or refresh token");
        }

#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        string username = principal.Identity.Name;
#pragma warning restore CS8602 // Dereference of a possibly null reference.
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.

        var user = await userManager.FindByNameAsync(username);

        if (user == null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
        {
            return Results.BadRequest("Invalid access token or refresh token");
        }

        var newAccessToken = CreateToken(principal.Claims.ToList(), config);
        var newRefreshToken = GenerateRefreshToken();

        user.RefreshToken = newRefreshToken;
        _ = int.TryParse(config["Jwt:RefreshTokenValidityInDays"], out int refreshTokenValidityInDays);
        var refreshTokenExpiresAt = DateTime.Now.AddDays(refreshTokenValidityInDays);
        user.RefreshTokenExpiryTime = refreshTokenExpiresAt;

        await userManager.UpdateAsync(user);

        return Results.Ok(new
        {
            Token = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
            TokenExpiresAt = newAccessToken.ValidTo,
            RefreshToken = newRefreshToken,
            RefreshTokenExpiresAt = refreshTokenExpiresAt
        });
    }

    private static string GenerateRefreshToken()
    {
        var randomNumber = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }

    private static ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token, IConfiguration config)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false,
            ValidateIssuer = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"])),
            ValidateLifetime = false
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
        if (securityToken is not JwtSecurityToken jwtSecurityToken ||
            !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                StringComparison.InvariantCultureIgnoreCase))
            throw new SecurityTokenException("Invalid token");

        return principal;
    }
}