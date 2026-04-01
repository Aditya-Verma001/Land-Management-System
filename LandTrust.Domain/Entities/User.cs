using System;
using System.Collections.Generic;
using System.Text;

using LandTrust.Domain.Enums;

namespace LandTrust.Domain.Entities;

public class User
{
    public Guid UserId { get; private set; }

    public string FullName { get; private set; }

    public string Email { get; private set; }

    public string GovernmentId { get; private set; }

    public UserRole Role { get; private set; }

    public bool IsKycVerified { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public User(
        string fullName,
        string email,
        string governmentId,
        UserRole role)
    {
        UserId = Guid.NewGuid();
        FullName = fullName;
        Email = email;
        GovernmentId = governmentId;
        Role = role;
        CreatedAt = DateTime.UtcNow;
        IsKycVerified = false;
    }

    public void VerifyKyc()
    {
        IsKycVerified = true;
    }
}
