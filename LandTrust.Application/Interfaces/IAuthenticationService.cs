using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LandTrust.Application.DTOs;

namespace LandTrust.Application.Interfaces;

public interface IAuthenticationService
{
    Task<RegisterUserResponseDto> Register(RegisterUserDto request);

    Task<LoginResponseDto> Login(LoginRequestDto request);
}
