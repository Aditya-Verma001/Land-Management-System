using System;
using System.Collections.Generic;
using System.Text;

namespace LandTrust.Application.DTOs;

public class FraudCheckRequestDto
{
    public Guid PropertyId { get; set; }

    public Guid SellerId { get; set; }

    public Guid BuyerId { get; set; }

    public double RequestedArea { get; set; }
}
