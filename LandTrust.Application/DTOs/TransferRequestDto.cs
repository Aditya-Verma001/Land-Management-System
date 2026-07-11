using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LandTrust.Application.DTOs;

public class TransferRequestDto
{
    [Required]
    public Guid PropertyId { get; set; }

    [Required]
    public Guid BuyerId { get; set; }
}