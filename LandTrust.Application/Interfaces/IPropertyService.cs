using LandTrust.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace LandTrust.Application.Interfaces;

public interface IPropertyService
{
    void TransferProperty(Guid propertyId, Guid sellerId, Guid buyerId);
    CreatePropertyResponseDto CreateProperty(CreatePropertyDto request);
}
