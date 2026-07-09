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

    public DbSet<TransferRequest> TransferRequests => Set<TransferRequest>();

    public DbSet<User> Users => Set<User>();

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

        modelBuilder.Entity<TransferRequest>(entity =>
        {
            entity.HasKey(x => x.RequestId);

            entity.Property(x => x.RiskLevel)
                  .HasMaxLength(20);

            entity.Property(x => x.OfficerRemarks)
                  .HasMaxLength(500);

            entity.Property(x => x.RejectionReason)
                  .HasMaxLength(500);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(x => x.UserId);

            entity.Property(x => x.FullName)
                  .HasMaxLength(150)
                  .IsRequired();

            entity.Property(x => x.Email)
                  .HasMaxLength(150)
                  .IsRequired();

            entity.HasIndex(x => x.Email)
                  .IsUnique();

            entity.Property(x => x.PasswordHash)
                  .IsRequired();

            entity.Property(x => x.GovernmentId)
                  .HasMaxLength(50);

            entity.Property(x => x.IsActive)
                  .HasDefaultValue(true);
        });
    }
}