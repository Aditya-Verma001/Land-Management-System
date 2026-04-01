using LandTrust.Application.DTOs;
using LandTrust.Application.Interfaces;
using LandTrust.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace LandTrust.Application.Services;

public class PropertyService : IPropertyService
{
    private readonly List<OwnershipRecord> _ownershipRecords = new();

    private readonly List<Property> _properties = new();

    public void TransferProperty(Guid propertyId, Guid sellerId, Guid buyerId)
    {

        var propertyExists = _properties.Any(p => p.PropertyId == propertyId);

        if (!propertyExists)
            throw new Exception("Property does not exist");

        // Step 1: Get current ownership
        var currentOwnership = _ownershipRecords
            .FirstOrDefault(x => x.PropertyId == propertyId && x.IsActive);

        if (currentOwnership == null)
            throw new Exception("Property has no active owner");

        // Step 2: Validate seller
        if (currentOwnership.OwnerUserId != sellerId)
            throw new Exception("Seller is not the owner");

        // Step 3: End current ownership
        currentOwnership.EndOwnership();

        // Step 4: Create new ownership
        var newOwnership = new OwnershipRecord(propertyId, buyerId);

        _ownershipRecords.Add(newOwnership);
    }

    public CreatePropertyResponseDto CreateProperty(CreatePropertyDto request)
    {
        var property = new Property(
            request.State,
            request.District,
            request.Village,
            request.SurveyNumber,
            request.Area,
            request.Latitude,
            request.Longitude
        );

        _properties.Add(property);

        // Create Ownership
        var ownership = new OwnershipRecord(property.PropertyId, request.OwnerId);

        // Store ownership
        _ownershipRecords.Add(ownership);

        return new CreatePropertyResponseDto
        {
            PropertyId = property.PropertyId,
            Message = "Property created successfully"
        };
    }
}


