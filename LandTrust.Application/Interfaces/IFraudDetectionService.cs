using LandTrust.Application.DTOs;

namespace LandTrust.Application.Interfaces;

public interface IFraudDetectionService
{
    Task<FraudCheckResultDto> CheckFraud(FraudCheckRequestDto request);
}