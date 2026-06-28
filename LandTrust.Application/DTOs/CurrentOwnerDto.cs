using System;
using System.Collections.Generic;
using System.Text;

namespace LandTrust.Application.DTOs;

public class CurrentOwnerDto
{
    public Guid PropertyId { get; set; }

    public Guid OwnerId { get; set; }

    public DateTime OwnershipStartDate { get; set; }
}
