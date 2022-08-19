using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace JobTrackerDataManager.data;

public class AppIdentityDbContext :
    IdentityDbContext<ApplicationUser, IdentityRole, string>
{
    public AppIdentityDbContext
        (DbContextOptions<AppIdentityDbContext> options)
        : base(options)
    {
    }
}