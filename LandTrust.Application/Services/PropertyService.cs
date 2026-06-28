using LandTrust.Application.DTOs;
using LandTrust.Application.Interfaces;
using LandTrust.Domain.Entities;
using LandTrust.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace LandTrust.Application.Services;

public class PropertyService : IPropertyService
{
    
    private readonly LandTrustDbContext _context;
    private readonly ILogger<PropertyService> _logger;

public PropertyService(LandTrustDbContext context, ILogger<PropertyService> logger)
    {
        _context = context;
        _logger = logger;
    }

public async Task<string> TransferProperty(TransferRequestDto request)
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
       

        if (currentOwner == null || currentOwner.OwnerUserId != request.SellerId)
        {
            _logger.LogWarning("Invalid seller. SellerId: {SellerId}", request.SellerId);
            return "Seller is not the current owner";
        }
            

        currentOwner.EndOwnership();

        var newOwner = new OwnershipRecord(request.PropertyId, request.BuyerId);
        _context.OwnershipRecords.Add(newOwner);

        if (request.SellerId == request.BuyerId)
            return "Seller and Buyer cannot be same";

        if (!currentOwner.IsActive)
            return "Property already transferred";

        await _context.SaveChangesAsync();

        _logger.LogInformation("Property transferred from {SellerId} to {BuyerId}",
        request.SellerId, request.BuyerId);

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
}


