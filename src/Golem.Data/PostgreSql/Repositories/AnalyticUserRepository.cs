﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Golem.Data.PostgreSql.Models;
using Golem.Data.PostgreSql.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Golem.Data.PostgreSql.Repositories
{
    public class AnalyticUserRepository
    {
        private readonly GolemContext dbContext;

        public AnalyticUserRepository(GolemContext context)
        {
            dbContext = context;
        }

        public async Task<IEnumerable<User>> Get(DateTime? lastVisitDateFrom, DateTime? lastVisitDateTo, int skip,
            int take)
        {
            var result =
                dbContext.AnalyticUsers
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
            return await dbContext.AnalyticUsers.FindAsync(id);
        }

        public async Task Create(User entity)
        {
            dbContext.AnalyticUsers.Add(entity);
            await dbContext.SaveChangesAsync();
        }

        public async Task Update(User entity)
        {
            dbContext.AnalyticUsers.Update(entity);
            await dbContext.SaveChangesAsync();
        }

        public async Task Delete(User entity)
        {
            dbContext.AnalyticUsers.Remove(entity);
            await dbContext.SaveChangesAsync();
        }

        public async Task<double> GetAverageNumberOfRequests()
        {
            return await dbContext.AnalyticUsers
                .Select(u => u.Queries.Count)
                .AverageAsync();
        }

        public async Task<int> GetCount(DateTime? lastVisitDateFrom, DateTime? lastVisitDateTo)
        {
            var result = dbContext.AnalyticUsers.AsQueryable();
            result = ApplyFiltering(lastVisitDateFrom, lastVisitDateTo, result);
            return await result.CountAsync();
        }

        public async Task<int> GetUserWithOneRequestCount()
        {
            return await dbContext.AnalyticUsers
                .Where(user => user.NumberOfRequests == 1)
                .CountAsync();
        }

        public async Task<IList<Country>> GetCountries()
        {
            return await dbContext.AnalyticUsers
                .GroupBy(user => user.Country)
                .Select(result => new Country
                {
                    Name = result.Key,
                    Number = result.Count()
                })
                .ToListAsync();
        }
        
        public async Task<IList<Browser>> GetBrowsers()
        {
            return await dbContext.AnalyticUsers
                .GroupBy(user => user.UserAgent)
                .Select(result => new Browser
                {
                    Name = result.Key,
                    Number = result.Count()
                })
                .ToListAsync();
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
