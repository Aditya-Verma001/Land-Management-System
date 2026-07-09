using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LandTrust.Domain.Entities;

namespace LandTrust.Application.Interfaces;

public interface IJwtTokenService
{
    string GenerateToken(User user);
}