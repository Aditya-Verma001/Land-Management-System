using LandTrust.Application.DTOs;
using LandTrust.Application.Interfaces;
using LandTrust.Domain.Entities;
using LandTrust.Infrastructure.Data;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace LandTrust.Application.Services;

public class AuditService : IAuditService
{
    private readonly LandTrustDbContext _context;
    private readonly ILogger<AuditService> _logger;

    public AuditService(
        LandTrustDbContext context,
        ILogger<AuditService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task LogAsync(AuditLogDto dto)
    {
        var log = new AuditLog(
            dto.Action,
            dto.Module,
            dto.UserId,
            dto.PropertyId,
            dto.Status,
            dto.Remarks,
            dto.IpAddress);

        _context.AuditLogs.Add(log);

        await _context.SaveChangesAsync();

        _logger.LogInformation(
            "Audit Logged : {Action} ({Status})",
            dto.Action,
            dto.Status);
    }
}