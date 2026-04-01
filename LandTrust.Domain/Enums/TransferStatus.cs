using System;
using System.Collections.Generic;
using System.Text;

namespace LandTrust.Domain.Enums;

public enum TransferStatus
{
    Initiated = 1,
    Verified = 2,
    Approved = 3,
    Completed = 4,
    Rejected = 5
}
