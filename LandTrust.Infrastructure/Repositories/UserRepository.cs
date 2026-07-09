using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LandTrust.Domain.Entities;
using LandTrust.Domain.Repositories;
using LandTrust.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LandTrust.Infrastructure.Repositories;

public class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(LandTrustDbContext context)
        : base(context)
    {
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _dbSet
            .FirstOrDefaultAsync(x => x.Email == email);
    }

    public async Task<bool> EmailExistsAsync(string email)
    {
        return await _dbSet
            .AnyAsync(x => x.Email == email);
    }

    
}