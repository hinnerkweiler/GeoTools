using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GeoTools.Data;

public class GeoDbContext : DbContext
{
    public GeoDbContext(DbContextOptions<GeoDbContext> options) : base(options) {}
    
    public DbSet<Models.LocationInfoDto> LocationInfos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
       
    }
}