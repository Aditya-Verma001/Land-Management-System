using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LandTrust.Domain.Entities;

namespace LandTrust.Domain.Repositories;

public interface IOwnershipRepository
{
    Task<OwnershipRecord?> GetCurrentOwnerAsync(Guid propertyId);

    Task<List<OwnershipRecord>> GetHistoryAsync(Guid propertyId);

    Task<List<OwnershipRecord>> GetActiveOwnershipsAsync();

    Task<List<OwnershipRecord>> GetInactiveOwnershipsAsync();

    Task<List<OwnershipRecord>> GetOwnershipsByDateAsync(
        DateTime from,
        DateTime to);

    Task AddAsync(OwnershipRecord ownership);

    Task SaveChangesAsync();
}
