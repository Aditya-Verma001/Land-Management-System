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

    public TransferRequest(Guid propertyId, Guid sellerId, Guid buyerId)
    {
        RequestId = Guid.NewGuid();
        PropertyId = propertyId;
        SellerId = sellerId;
        BuyerId = buyerId;
        Status = TransferStatus.Initiated;
        CreatedAt = DateTime.UtcNow;
    }

    public void Verify()
    {
        if (Status != TransferStatus.Initiated)
            throw new Exception("Invalid state transition");

        Status = TransferStatus.Verified;
    }

    public void Approve()
    {
        if (Status != TransferStatus.Verified)
            throw new Exception("Must be verified first");

        Status = TransferStatus.Approved;
    }

    public void Complete()
    {
        if (Status != TransferStatus.Approved)
            throw new Exception("Must be approved first");

        Status = TransferStatus.Completed;
    }
}
