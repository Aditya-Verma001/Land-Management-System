using System;
using System.Collections.Generic;
using System.Text;

namespace LandTrust.Domain.Entities;

public class OwnershipRecord
{
    public Guid OwnershipRecordId { get; private set; }

    public Guid PropertyId { get; private set; }

    public Guid OwnerUserId { get; private set; }

    public DateTime FromDate { get; private set; }

    public DateTime? ToDate { get; private set; }

    public bool IsActive { get; private set; }

    public OwnershipRecord(Guid propertyId, Guid ownerUserId)
    {
        OwnershipRecordId = Guid.NewGuid();
        PropertyId = propertyId;
        OwnerUserId = ownerUserId;
        FromDate = DateTime.UtcNow;
        IsActive = true;
    }

    public void EndOwnership()
    {
        ToDate = DateTime.UtcNow;
        IsActive = false;
    }
}
