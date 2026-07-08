using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LandTrust.Domain.Enums;

namespace LandTrust.Application.DTOs;

public class PendingTransferDto
{
    public Guid RequestId { get; set; }

    public Guid PropertyId { get; set; }

    public Guid SellerId { get; set; }

    public Guid BuyerId { get; set; }

    public TransferStatus Status { get; set; }

    public int FraudScore { get; set; }

    public string RiskLevel { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }
}