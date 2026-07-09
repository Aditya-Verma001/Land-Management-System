using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using LandTrust.Domain.Enums;

namespace LandTrust.Application.DTOs;

public class RegisterUserDto
{
    [Required]
    public string FullName { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string Password { get; set; } = string.Empty;

    [Required]
    public string GovernmentId { get; set; } = string.Empty;

    [Required]
    public UserRole Role { get; set; }
}