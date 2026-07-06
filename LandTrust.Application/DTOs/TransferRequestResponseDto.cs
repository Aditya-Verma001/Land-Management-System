using System;
using System.Collections.Generic;
using System.Text;

using LandTrust.Domain.Enums;

namespace LandTrust.Application.DTOs;

public class TransferRequestResponseDto
{
    public Guid RequestId { get; set; }

    public bool Success { get; set; }

    public int FraudScore { get; set; }

    public string RiskLevel { get; set; } = string.Empty;

    public TransferStatus Status { get; set; }

    public string Message { get; set; } = string.Empty;
}