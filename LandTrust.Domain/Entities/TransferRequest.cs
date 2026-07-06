using System;
using System.Collections.Generic;
using System.Text;
using LandTrust.Domain.Enums;

namespace LandTrust.Domain.Entities;

public class TransferRequest
{
    public Guid RequestId { get; private set; }

    public Guid PropertyId { get; private set; }

    public Guid SellerId { get; private set; }

    public Guid BuyerId { get; private set; }

    public TransferStatus Status { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public int FraudScore { get; private set; }

    public string RiskLevel { get; private set; } = "Low";

    public string? OfficerRemarks { get; private set; }

    public Guid? VerifiedBy { get; private set; }

    public Guid? ApprovedBy { get; private set; }

    public DateTime? VerifiedAt { get; private set; }

    public DateTime? ApprovedAt { get; private set; }

    public DateTime? CompletedAt { get; private set; }

    public string? RejectionReason { get; private set; }

    public TransferRequest(Guid propertyId, Guid sellerId, Guid buyerId)
    {
        RequestId = Guid.NewGuid();
        PropertyId = propertyId;
        SellerId = sellerId;
        BuyerId = buyerId;
        Status = TransferStatus.Initiated;
        CreatedAt = DateTime.UtcNow;
    }

    public void SetFraudAssessment(int score)
    {
        FraudScore = score;

        if (score <= 20)
            RiskLevel = "Low";
        else if (score <= 50)
            RiskLevel = "Medium";
        else
            RiskLevel = "High";
    }

    public void Verify(Guid officerId)
    {
        if (Status != TransferStatus.Initiated)
            throw new Exception("Invalid state transition");

        VerifiedBy = officerId;
        VerifiedAt = DateTime.UtcNow;

        Status = TransferStatus.Verified;
    }

    public void Approve(Guid officerId, string remarks)
    {
        if (Status != TransferStatus.Verified)
            throw new Exception("Must be verified first");

        ApprovedBy = officerId;
        OfficerRemarks = remarks;
        ApprovedAt = DateTime.UtcNow;

        Status = TransferStatus.Approved;
    }

    public void Complete()
    {
        if (Status != TransferStatus.Approved)
            throw new Exception("Must be approved first");

        CompletedAt = DateTime.UtcNow;

        Status = TransferStatus.Completed;
    }

    public void Reject(Guid officerId, string reason)
    {
        if (Status != TransferStatus.Verified)
            throw new Exception("Only verified requests can be rejected.");

        ApprovedBy = officerId;
        RejectionReason = reason;
        ApprovedAt = DateTime.UtcNow;

        Status = TransferStatus.Rejected;
    }
}
