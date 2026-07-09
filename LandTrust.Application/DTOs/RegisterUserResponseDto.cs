using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LandTrust.Application.DTOs;

public class RegisterUserResponseDto
{
    public bool Success { get; set; }

    public string Message { get; set; } = string.Empty;

    public Guid UserId { get; set; }
}