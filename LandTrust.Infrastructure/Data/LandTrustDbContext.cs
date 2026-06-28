using LandTrust.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LandTrust.Infrastructure.Data;

public class LandTrustDbContext : DbContext
{
    public LandTrustDbContext(DbContextOptions<LandTrustDbContext> options)
        : base(options)
    {
    }

    public DbSet<Property> Properties { get; set; }

    public DbSet<OwnershipRecord> OwnershipRecords { get; set; }
}