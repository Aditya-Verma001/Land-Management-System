using System;
using System.Collections.Generic;
using System.Text;

using LandTrust.Domain.Entities;

namespace LandTrust.Domain.Repositories;

public interface IPropertyRepository : IRepository<Property>
{
    Task<bool> SurveyNumberExistsAsync(string surveyNumber);

    Task<Property?> GetBySurveyNumberAsync(string surveyNumber);

    Task<Property?> GetPropertyWithOwnershipAsync(Guid propertyId);
}