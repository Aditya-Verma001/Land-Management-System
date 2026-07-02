using System;
using System.Collections.Generic;
using System.Text;

using LandTrust.Application.DTOs;
using LandTrust.Application.Interfaces;
using LandTrust.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LandTrust.Application.Services;

public class FraudDetectionService : IFraudDetectionService
{
    private readonly LandTrustDbContext _context;

    public FraudDetectionService(LandTrustDbContext context)
    {
        _context = context;
    }

    public async Task<FraudCheckResultDto> AnalyzeAsync(FraudCheckRequestDto request)
    {
        var result = new FraudCheckResultDto();

        int riskScore = 0;

        // Rule 1 - Seller Validation
        var currentOwner = await _context.OwnershipRecords
            .FirstOrDefaultAsync(x =>
                x.PropertyId == request.PropertyId &&
                x.IsActive);

        if (currentOwner == null || currentOwner.OwnerUserId != request.SellerId)
        {
            riskScore += 30;
            result.Reasons.Add("Seller is not the current owner.");
        }

        // Rule 2 - Property Exists
        var property = await _context.Properties
            .FirstOrDefaultAsync(x => x.PropertyId == request.PropertyId);

        if (property == null)
        {
            riskScore += 40;
            result.Reasons.Add("Property does not exist.");
        }
        else
        {
            // Rule 3 - Area Validation
            if (Math.Abs(property.Area - request.RequestedArea) > 0.01)
            {
                riskScore += 20;
                result.Reasons.Add("Area mismatch detected.");
            }

            // Rule 4 - Invalid Coordinates
            if (property.Latitude == 0 || property.Longitude == 0)
            {
                riskScore += 10;
                result.Reasons.Add("Invalid property coordinates.");
            }
        }

        result.RiskScore = riskScore;
        result.IsFraudDetected = riskScore >= 50;

        result.Recommendation = riskScore switch
        {
            <= 20 => "Safe",
            <= 50 => "Medium Risk - Officer Review",
            _ => "High Risk - Manual Verification Required"
        };

        return result;
    }
}