using Microsoft.EntityFrameworkCore;
using WeatherByZipAPI.Models;

namespace WeatherByZip.API;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

    public DbSet<CityInfo> CityInfos => Set<CityInfo>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<CityInfo>()
            .Property(x => x.RequestDate).HasDefaultValueSql("GetDate()");
    }
}
