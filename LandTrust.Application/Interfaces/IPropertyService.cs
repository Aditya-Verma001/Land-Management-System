using LandTrust.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace LandTrust.Application.Interfaces;

public interface IPropertyService
{
    Task<string> TransferProperty(TransferRequestDto request);
    Task<CreatePropertyResponseDto> CreateProperty(CreatePropertyDto request);
    Task<List<PropertyHistoryDto>> GetPropertyHistory(Guid propertyId);

    Task<CurrentOwnerDto?> GetCurrentOwner(Guid propertyId);
    Task<List<PropertyHistoryDto>> GetActiveOwnerships();

    Task<List<PropertyHistoryDto>> GetInactiveOwnerships();

    Task<List<PropertyHistoryDto>> GetOwnershipHistory(DateTime from, DateTime to);
}
