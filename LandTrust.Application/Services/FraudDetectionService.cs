using LandTrust.Application.DTOs;
using LandTrust.Application.Interfaces;

namespace LandTrust.Application.Services;

public class FraudDetectionService : IFraudDetectionService
{
    public Task<FraudCheckResultDto> CheckFraud(
        FraudCheckRequestDto request)
    {
        int score = 0;

        if (request.SellerId == request.BuyerId)
            score += 70;

        if (request.PropertyAgeInDays < 30)
            score += 20;

        if (request.PreviousTransfers > 3)
            score += 30;

        string risk = "Low";

        if (score > 50)
            risk = "High";
        else if (score > 20)
            risk = "Medium";

        return Task.FromResult(new FraudCheckResultDto
        {
            FraudScore = score,
            RiskLevel = risk
        });
    }
}