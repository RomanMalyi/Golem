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

        public async Task<IEnumerable<User>> Get(DateTime? lastVisitDateFrom, DateTime? lastVisitDateTo, int skip,
            int take)
        {
            var result =
                dbContext.Users
                    .OrderByDescending(u => u.NumberOfVisits)
                    .AsQueryable();
            result = ApplyFiltering(lastVisitDateFrom, lastVisitDateTo, result);

            return await result
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

        public async Task<int> GetCount(DateTime? lastVisitDateFrom, DateTime? lastVisitDateTo)
        {
            var result = dbContext.Users.AsQueryable();
            result = ApplyFiltering(lastVisitDateFrom, lastVisitDateTo, result);
            return await result.CountAsync();
        }

        public async Task<int> GetUserWithOneRequestCount()
        {
            return await dbContext.Users
                .Where(user => user.NumberOfRequests == 1)
                .CountAsync();
        }

        private static IQueryable<User> ApplyFiltering(DateTime? lastVisitDateFrom, DateTime? lastVisitDateTo,
            IQueryable<User> result)
        {
            if (lastVisitDateFrom.HasValue)
                result = result
                    .Where(user => user.LastVisitTime >= lastVisitDateFrom.Value);

            if (lastVisitDateTo.HasValue)
                result = result
                    .Where(user => user.LastVisitTime <= lastVisitDateTo.Value);

            return result;
        }
    }
}
