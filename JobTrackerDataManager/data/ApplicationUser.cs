using Microsoft.AspNetCore.Identity;

namespace JobTrackerDataManager.data;

public class ApplicationUser : IdentityUser
{
    public string? RefreshToken { get; set; }
    public DateTime RefreshTokenExpiryTime { get; set; }
}