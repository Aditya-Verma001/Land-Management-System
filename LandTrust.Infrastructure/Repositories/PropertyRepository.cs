using LandTrust.Domain.Entities;
using LandTrust.Domain.Repositories;
using LandTrust.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LandTrust.Infrastructure.Repositories;

public class PropertyRepository : Repository<Property>, IPropertyRepository
{
    public PropertyRepository(LandTrustDbContext context)
        : base(context)
    {
    }

    public async Task<bool> SurveyNumberExistsAsync(string surveyNumber)
    {
        return await _dbSet.AnyAsync(x => x.SurveyNumber == surveyNumber);
    }

    public async Task<Property?> GetBySurveyNumberAsync(string surveyNumber)
    {
        return await _dbSet
            .FirstOrDefaultAsync(x => x.SurveyNumber == surveyNumber);
    }

    public async Task<Property?> GetPropertyWithOwnershipAsync(Guid propertyId)
    {
        return await _dbSet
            .Include(x => x.OwnershipRecords)
            .FirstOrDefaultAsync(x => x.PropertyId == propertyId);
    }
}