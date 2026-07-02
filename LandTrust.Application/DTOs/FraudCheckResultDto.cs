using System;
using System.Collections.Generic;
using System.Text;

namespace LandTrust.Application.DTOs;

public class FraudCheckResultDto
{
    public bool IsFraudDetected { get; set; }

    public int RiskScore { get; set; }

    public List<string> Reasons { get; set; } = new();

    public string Recommendation { get; set; } = string.Empty;
}