using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LandTrust.Domain.Enums;

namespace LandTrust.Application.DTOs;

public class LoginResponseDto
{
    public bool Success { get; set; }

    public string Message { get; set; } = string.Empty;

    public string Token { get; set; } = string.Empty;

    public Guid UserId { get; set; }

    public string FullName { get; set; } = string.Empty;

    public UserRole Role { get; set; }
}