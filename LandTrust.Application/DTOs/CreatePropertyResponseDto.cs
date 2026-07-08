using System;
using System.Collections.Generic;
using System.Text;

namespace LandTrust.Application.DTOs;

public class CreatePropertyResponseDto
{
    public Guid PropertyId { get; set; }

    public string Message { get; set; } = string.Empty;
}
