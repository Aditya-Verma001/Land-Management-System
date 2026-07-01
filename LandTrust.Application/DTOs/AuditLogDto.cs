using System;
using System.Collections.Generic;
using System.Text;

namespace LandTrust.Application.DTOs;

public class AuditLogDto
{
    public string Action { get; set; } = string.Empty;

    public string Module { get; set; } = string.Empty;

    public Guid? UserId { get; set; }

    public Guid? PropertyId { get; set; }

    public string Status { get; set; } = string.Empty;

    public string Remarks { get; set; } = string.Empty;

    public string? IpAddress { get; set; }
}