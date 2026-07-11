using LandTrust.Application.DTOs;
using LandTrust.Application.Interfaces;
using LandTrust.Domain.Entities;
using LandTrust.Domain.Enums;
using LandTrust.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace LandTrust.Application.Services;


public class PropertyService : IPropertyService
{
    private readonly LandTrustDbContext _context;
    private readonly ILogger<PropertyService> _logger;
    private readonly IAuditService _auditService;
    private readonly IFraudDetectionService _fraudService;

    public PropertyService(
    LandTrustDbContext context,
    ILogger<PropertyService> logger,
    IAuditService auditService,
    IFraudDetectionService fraudService)
    {
        _context = context;
        _logger = logger;
        _auditService = auditService;
        _fraudService = fraudService;
    }

    public async Task<string> TransferProperty(Guid sellerId, TransferRequestDto request)
    {
        _logger.LogInformation("Transfer started for PropertyId: {PropertyId}", request.PropertyId);

        var property = await _context.Properties
            .FirstOrDefaultAsync(p => p.PropertyId == request.PropertyId);

       
        if (property == null)
        {
            _logger.LogWarning("Property not found: {PropertyId}", request.PropertyId);
            return "Property does not exist";
        }

        var currentOwner = await _context.OwnershipRecords
            .Where(o => o.PropertyId == request.PropertyId && o.IsActive)
            .FirstOrDefaultAsync();
       

        if (currentOwner == null || currentOwner.OwnerUserId != sellerId)
        {
            _logger.LogWarning("Invalid seller. SellerId: {SellerId}", sellerId);
            return "Seller is not the current owner";
        }
            

        currentOwner.EndOwnership();

        var newOwner = new OwnershipRecord(request.PropertyId, request.BuyerId);
        _context.OwnershipRecords.Add(newOwner);

        if (sellerId == request.BuyerId)
            return "Seller and Buyer cannot be same";

        if (!currentOwner.IsActive)
            return "Property already transferred";

        await _context.SaveChangesAsync();

        await _auditService.LogAsync(new AuditLogDto
        {
            Action = "Transfer Property",
            Module = "Property",
            UserId = sellerId,
            PropertyId = request.PropertyId,
            Status = "Success",
            Remarks = $"Transferred to {request.BuyerId}"
        });

        await _auditService.LogAsync(new AuditLogDto
        {
            Action = "Transfer Property",

            Module = "Property",

            UserId = sellerId,

            PropertyId = request.PropertyId,

            Status = "Failed",

            Remarks = "Property not found."
        });

        _logger.LogInformation("Property transferred from {SellerId} to {BuyerId}",
       sellerId, request.BuyerId);

        return "Property transferred successfully";
    }

    public async Task<CreatePropertyResponseDto> CreateProperty(CreatePropertyDto request)
    {

        _logger.LogInformation("Creating property for OwnerId: {OwnerId}", request.OwnerId);

        var property = new Property(
            request.State,
            request.District,
            request.Village,
            request.SurveyNumber,
            request.Area,
            request.Latitude,
            request.Longitude
        );

        _context.Properties.Add(property);

        var ownership = new OwnershipRecord(property.PropertyId, request.OwnerId);
        _context.OwnershipRecords.Add(ownership);

        await _context.SaveChangesAsync();

        await _auditService.LogAsync(new AuditLogDto
        {
            Action = "Create Property",
            Module = "Property",
            UserId = request.OwnerId,
            PropertyId = property.PropertyId,
            Status = "Success",
            Remarks = $"Property {property.SurveyNumber} created successfully."
        });

        _logger.LogInformation("Property created successfully with Id: {PropertyId}", property.PropertyId);

        return new CreatePropertyResponseDto
        {
            PropertyId = property.PropertyId,
            Message = "Property created successfully"
        };
    }

    //public CreatePropertyResponseDto CreateProperty(CreatePropertyDto request)
    //{
    //    var property = new Property(
    //        request.State,
    //        request.District,
    //        request.Village,
    //        request.SurveyNumber,
    //        request.Area,
    //        request.Latitude,
    //        request.Longitude
    //    );

    //    _properties.Add(property);

    //    // Create Ownership
    //    var ownership = new OwnershipRecord(property.PropertyId, request.OwnerId);

    //    // Store ownership
    //    _ownershipRecords.Add(ownership);

    //    return new CreatePropertyResponseDto
    //    {
    //        PropertyId = property.PropertyId,
    //        Message = "Property created successfully"
    //    };
    //}

    public async Task<List<PropertyHistoryDto>> GetPropertyHistory(Guid propertyId)
    {
        var history = await _context.OwnershipRecords
            .Where(o => o.PropertyId == propertyId)
            .OrderBy(o => o.FromDate)
            .Select(o => new PropertyHistoryDto
            {
                OwnerUserId = o.OwnerUserId,
                FromDate = o.FromDate,
                ToDate = o.ToDate,
                IsActive = o.IsActive
            })
            .ToListAsync();

        return history;
    }

    public async Task<CurrentOwnerDto?> GetCurrentOwner(Guid propertyId)
    {
        var owner = await _context.OwnershipRecords
            .Where(o => o.PropertyId == propertyId && o.IsActive)
            .Select(o => new CurrentOwnerDto
            {
                PropertyId = o.PropertyId,
                OwnerId = o.OwnerUserId,
                OwnershipStartDate = o.FromDate
            })
            .FirstOrDefaultAsync();

        return owner;
    }

    public async Task<List<PropertyHistoryDto>> GetActiveOwnerships()
    {
        return await _context.OwnershipRecords
            .Where(x => x.IsActive)
            .Select(x => new PropertyHistoryDto
            {
                OwnerUserId = x.OwnerUserId,
                FromDate = x.FromDate,
                ToDate = x.ToDate,
                IsActive = x.IsActive
            })
            .ToListAsync();
    }

    public async Task<List<PropertyHistoryDto>> GetInactiveOwnerships()
    {
        return await _context.OwnershipRecords
            .Where(x => !x.IsActive)
            .Select(x => new PropertyHistoryDto
            {
                OwnerUserId = x.OwnerUserId,
                FromDate = x.FromDate,
                ToDate = x.ToDate,
                IsActive = x.IsActive
            })
            .ToListAsync();
    }

    public async Task<List<PropertyHistoryDto>> GetOwnershipHistory(DateTime from, DateTime to)
    {
        return await _context.OwnershipRecords
            .Where(x => x.FromDate >= from &&
                        x.FromDate <= to)
            .Select(x => new PropertyHistoryDto
            {
                OwnerUserId = x.OwnerUserId,
                FromDate = x.FromDate,
                ToDate = x.ToDate,
                IsActive = x.IsActive
            })
            .ToListAsync();
    }

    public async Task<TransferRequestResponseDto> SubmitTransferRequest(
    SubmitTransferRequestDto request)
    {
        _logger.LogInformation(
            "Transfer Request submitted for Property {PropertyId}",
            request.PropertyId);

        // Check property exists
        var property = await _context.Properties
            .FirstOrDefaultAsync(x => x.PropertyId == request.PropertyId);

        int propertyAgeInDays = 365; // Default value

        // If your Property entity has CreatedAt, use it.
        if (property != null)
        {
            // Replace CreatedAt with your actual property creation date field if different.
            // propertyAgeInDays = (DateTime.UtcNow - property.CreatedAt).Days;
        }

        int previousTransfers = await _context.OwnershipRecords
            .CountAsync(x => x.PropertyId == request.PropertyId);

        if (property == null)
        {
            return new TransferRequestResponseDto
            {
                Success = false,
                Message = "Property not found"
            };
        }

        // Check current owner
        var owner = await _context.OwnershipRecords
            .FirstOrDefaultAsync(x =>
                x.PropertyId == request.PropertyId &&
                x.IsActive);

        if (owner == null || owner.OwnerUserId != request.SellerId)
        {
            return new TransferRequestResponseDto
            {
                Success = false,
                Message = "Seller is not current owner"
            };
        }

        // Create request
        var transferRequest = new TransferRequest(
            request.PropertyId,
            request.SellerId,
            request.BuyerId);

        var fraudResult = await _fraudService.CheckFraud(
            new FraudCheckRequestDto
            {
                PropertyId = request.PropertyId,
                PropertyAgeInDays = propertyAgeInDays,
                PreviousTransfers = previousTransfers
            });

        // Save fraud assessment into transfer request
        transferRequest.SetFraudAssessment(
            fraudResult.FraudScore);

        if (fraudResult.RiskLevel == "High")
        {
            await _auditService.LogAsync(new AuditLogDto
            {
                Action = "Transfer Request",
                Module = "Fraud Detection",
                UserId = request.SellerId,
                PropertyId = request.PropertyId,
                Status = "Blocked",
                Remarks = "High fraud risk detected"
            });

            return new TransferRequestResponseDto
            {
                Success = false,
                Message = "Transfer request blocked due to high fraud risk."
            };
        }

        _context.TransferRequests.Add(transferRequest);

        await _context.SaveChangesAsync();

        await _auditService.LogAsync(new AuditLogDto
        {
            Action = "Transfer Request",
            Module = "Transfer",
            UserId = request.SellerId,
            PropertyId = request.PropertyId,
            Status = "Pending",
            Remarks = "Transfer request submitted"
        });

        return new TransferRequestResponseDto
        {
            Success = true,
            RequestId = transferRequest.RequestId,
            Status = transferRequest.Status,
            Message = "Transfer Request Submitted"
        };
    }

    public async Task<TransferRequestResponseDto> VerifyTransferRequest(Guid requestId, Guid officerId)
    {
        var request = await _context.TransferRequests
            .FirstOrDefaultAsync(x => x.RequestId == requestId);

        if (request == null)
        {
            return new TransferRequestResponseDto
            {
                Success = false,
                Message = "Transfer Request not found"
            };
        }

        request.Verify(officerId); ;

        await _context.SaveChangesAsync();

        return new TransferRequestResponseDto
        {
            Success = true,
            RequestId = request.RequestId,
            Status = request.Status,
            Message = "Transfer Request Verified"
        };
    }

    //public async Task<TransferRequestResponseDto> VerifyTransferRequest(Guid requestId)
    //{
    //    throw new NotImplementedException();
    //}
    public async Task<TransferRequestResponseDto> ApproveTransferRequest(Guid requestId, Guid officerId, string remarks)
    {
        throw new NotImplementedException();
    }
    public async Task<TransferRequestResponseDto> CompleteTransferRequest(Guid requestId)
    {
        // Find Transfer Request
        var request = await _context.TransferRequests
            .FirstOrDefaultAsync(x => x.RequestId == requestId);

        if (request == null)
        {
            return new TransferRequestResponseDto
            {
                Success = false,
                Message = "Transfer Request not found"
            };
        }

        // Find Current Owner
        var currentOwner = await _context.OwnershipRecords
            .FirstOrDefaultAsync(x =>
                x.PropertyId == request.PropertyId &&
                x.IsActive);

        if (currentOwner == null)
        {
            return new TransferRequestResponseDto
            {
                Success = false,
                Message = "Current owner not found"
            };
        }

        // End old ownership
        currentOwner.EndOwnership();

        // Create new ownership
        var newOwner = new OwnershipRecord(
            request.PropertyId,
            request.BuyerId);

        _context.OwnershipRecords.Add(newOwner);

        // Complete transfer
        request.Complete();

        await _context.SaveChangesAsync();

        // Audit log
        await _auditService.LogAsync(new AuditLogDto
        {
            Action = "Transfer Completed",
            Module = "Transfer",
            UserId = request.BuyerId,
            PropertyId = request.PropertyId,
            Status = "Success",
            Remarks = "Ownership transferred successfully"
        });

        return new TransferRequestResponseDto
        {
            Success = true,
            RequestId = request.RequestId,
            Status = request.Status,
            Message = "Transfer completed successfully"
        };
    }

    public async Task<List<PendingTransferDto>> GetPendingTransfers()
    {
        return await _context.TransferRequests
            .Where(x =>
                x.Status == TransferStatus.Initiated ||
                x.Status == TransferStatus.Verified)
            .OrderBy(x => x.CreatedAt)
            .Select(x => new PendingTransferDto
            {
                RequestId = x.RequestId,
                PropertyId = x.PropertyId,
                SellerId = x.SellerId,
                BuyerId = x.BuyerId,
                Status = x.Status,
                FraudScore = x.FraudScore,
                RiskLevel = x.RiskLevel,
                CreatedAt = x.CreatedAt
            })
            .ToListAsync();
    }

    public async Task<OfficerDashboardDto> GetDashboardAsync()
    {
        return new OfficerDashboardDto
        {
            PendingRequests = await _context.TransferRequests
                .CountAsync(x => x.Status == TransferStatus.Initiated),

            VerifiedRequests = await _context.TransferRequests
                .CountAsync(x => x.Status == TransferStatus.Verified),

            ApprovedRequests = await _context.TransferRequests
                .CountAsync(x => x.Status == TransferStatus.Approved),

            RejectedRequests = await _context.TransferRequests
                .CountAsync(x => x.Status == TransferStatus.Rejected),

            HighRiskRequests = await _context.TransferRequests
                .CountAsync(x => x.RiskLevel == "High"),

            MediumRiskRequests = await _context.TransferRequests
                .CountAsync(x => x.RiskLevel == "Medium"),

            LowRiskRequests = await _context.TransferRequests
                .CountAsync(x => x.RiskLevel == "Low")
        };
    }
}


