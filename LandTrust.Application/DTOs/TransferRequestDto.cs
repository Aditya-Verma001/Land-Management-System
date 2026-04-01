using System;
using System.Collections.Generic;
using System.Text;

namespace LandTrust.Application.DTOs;

public class TransferRequestDto
{
    public Guid PropertyId { get; set; }

    public Guid SellerId { get; set; }

    public Guid BuyerId { get; set; }
}