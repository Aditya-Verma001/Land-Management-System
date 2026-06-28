using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace LandTrust.Infrastructure.Data;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<LandTrustDbContext>
{
    public LandTrustDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<LandTrustDbContext>();

        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=LandTrustDb;Username=postgres;Password=jaimahakaal");

        return new LandTrustDbContext(optionsBuilder.Options);
    }
}