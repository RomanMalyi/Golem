using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Golem.Data.PostgreSql.Models;
using Microsoft.EntityFrameworkCore;

namespace Golem.Data.PostgreSql.Repositories
{
    public class UserRepository
    {
        private readonly GolemContext dbContext;

        public UserRepository(GolemContext context)
        {
            dbContext = context;
        }

        public async Task<IEnumerable<User>> Get(int skip, int take)
        {
            return await dbContext.Users
                .OrderByDescending(u => u.NumberOfVisits)
                .Skip(skip)
                .Take(take)
                .ToListAsync();
        }

        public async Task<User> GetById(Guid id)
        {
            return await dbContext.Users.FindAsync(id);
        }

        public async Task Create(User entity)
        {
            dbContext.Users.Add(entity);
            await dbContext.SaveChangesAsync();
        }

        public async Task Update(User entity)
        {
            dbContext.Users.Update(entity);
            await dbContext.SaveChangesAsync();
        }

        public async Task Delete(User entity)
        {
            dbContext.Users.Remove(entity);
            await dbContext.SaveChangesAsync();
        }

        public async Task<double> GetAverageNumberOfRequests()
        {
            return await dbContext.Users
                .Select(u => u.Queries.Count)
                .AverageAsync();
        }

        public async Task<int> GetCount()
        {
            return await dbContext.Users.CountAsync();
        }
    }
}
