using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using FormEODY.DataAccess.Entities;

namespace FormEODY.DataAccess;

public class AppDbContext : IdentityDbContext<ApplicationUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Application> Applications { get; set; } = default!;
    public DbSet<Occupation> Occupations { get; set; } = default!;
}
