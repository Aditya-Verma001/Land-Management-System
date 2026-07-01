using System;
using System.Collections.Generic;
using System.Text;
namespace LandTrust.Domain.Entities;

public class AuditLog
{
    public Guid AuditId { get; private set; }

    public DateTime Timestamp { get; private set; }

    public string Action { get; private set; }

    public string Module { get; private set; }

    public Guid? UserId { get; private set; }

    public Guid? PropertyId { get; private set; }

    public string Status { get; private set; }

    public string Remarks { get; private set; }

    public string? IpAddress { get; private set; }

    private AuditLog() { } // Required by EF Core

    public AuditLog(
        string action,
        string module,
        Guid? userId,
        Guid? propertyId,
        string status,
        string remarks,
        string? ipAddress = null)
    {
        AuditId = Guid.NewGuid();
        Timestamp = DateTime.UtcNow;

        Action = action;
        Module = module;

        UserId = userId;
        PropertyId = propertyId;

        Status = status;
        Remarks = remarks;

        IpAddress = ipAddress;
    }
}
