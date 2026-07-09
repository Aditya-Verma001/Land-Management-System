using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BCrypt.Net;
using LandTrust.Application.DTOs;
using LandTrust.Application.Interfaces;
using LandTrust.Domain.Entities;
using LandTrust.Domain.Repositories;

namespace LandTrust.Application.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly IUserRepository _userRepository;

    public AuthenticationService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<RegisterUserResponseDto> Register(RegisterUserDto request)
    {
        // Check duplicate email
        if (await _userRepository.EmailExistsAsync(request.Email))
        {
            return new RegisterUserResponseDto
            {
                Success = false,
                Message = "Email already registered."
            };
        }

        // Hash password
        var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

        // Create user
        var user = new User(
            request.FullName,
            request.Email,
            passwordHash,
            request.GovernmentId,
            request.Role);

        await _userRepository.AddAsync(user);
        await _userRepository.SaveChangesAsync();

        return new RegisterUserResponseDto
        {
            Success = true,
            UserId = user.UserId,
            Message = "User registered successfully."
        };
    }
}
