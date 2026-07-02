using System;
using System.Collections.Generic;
using System.Text;
using LandTrust.Application.DTOs;

namespace LandTrust.Application.Interfaces;

public interface IFraudDetectionService
{
    Task<FraudCheckResultDto> AnalyzeAsync(FraudCheckRequestDto request);
}