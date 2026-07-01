using LandTrust.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LandTrust.Infrastructure.Data;

public class LandTrustDbContext : DbContext
{
    public LandTrustDbContext(DbContextOptions<LandTrustDbContext> options)
        : base(options)
    {
    }

    public DbSet<Property> Properties => Set<Property>();

    public DbSet<OwnershipRecord> OwnershipRecords => Set<OwnershipRecord>();

    public DbSet<AuditLog> AuditLogs => Set<AuditLog>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Existing configurations...
        modelBuilder.Entity<AuditLog>(entity =>
        {
            entity.HasKey(x => x.AuditId);

            entity.Property(x => x.Action)
                  .IsRequired()
                  .HasMaxLength(100);

            entity.Property(x => x.Module)
                  .IsRequired()
                  .HasMaxLength(100);

            entity.Property(x => x.Status)
                  .IsRequired()
                  .HasMaxLength(50);

            entity.Property(x => x.Remarks)
                  .HasMaxLength(500);

            entity.Property(x => x.IpAddress)
                  .HasMaxLength(50);
        });
    }
}