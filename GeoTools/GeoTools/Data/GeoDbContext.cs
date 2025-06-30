using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GeoTools.Data;

public class GeoDbContext : DbContext
{
    public GeoDbContext(DbContextOptions<GeoDbContext> options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Optional: if you want to define DbSet<>s later
    }
}