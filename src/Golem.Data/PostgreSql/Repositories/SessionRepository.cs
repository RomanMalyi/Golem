using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Golem.Data.PostgreSql.Models;
using Microsoft.EntityFrameworkCore;

namespace Golem.Data.PostgreSql.Repositories
{
    public class SessionRepository
    {
        private readonly GolemContext dbContext;

        public SessionRepository(GolemContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Session> GetById(Guid id)
        {
            return await dbContext.Sessions.FindAsync(id);
        }

        public async Task<Session> GetLast(Guid userid)
        {
            return await dbContext.Sessions
                .OrderByDescending(q => q.EndTime)
                .Where(q => q.UserId == userid)
                .FirstAsync();
        }

        public async Task<IEnumerable<Session>> GetByUserId(Guid userid, DateTime? startDateFrom, DateTime? startDateTo,
            int skip, int take)
        {
            var result = dbContext.Sessions
                .OrderBy(q => q.EndTime)
                .Where(q => q.UserId == userid);
            result = ApplyFiltering(startDateFrom, startDateTo, result);

            return await result
                .Skip(skip)
                .Take(take)
                .ToListAsync();
        }

        public async Task Create(Session entity)
        {
            dbContext.Sessions.Add(entity);
            await dbContext.SaveChangesAsync();
        }

        public async Task Update(Session entity)
        {
            dbContext.Sessions.Update(entity);
            await dbContext.SaveChangesAsync();
        }

        public async Task Delete(Session entity)
        {
            dbContext.Sessions.Remove(entity);
            await dbContext.SaveChangesAsync();
        }

        public async Task<double> GetAverageSessionDuration()
        {
            //TODO: Calculate by small portioning in service
            var sessions = await dbContext.Sessions
                .Select(s => s.EndTime - s.StartTime)
                .ToListAsync();
            return sessions.Average(s => s.TotalMinutes);
        }

        public async Task<int> GetCount()
        {
            return await dbContext.Sessions.CountAsync();
        }

        public async Task<int> GetCount(Guid userId, DateTime? startDateFrom,
            DateTime? startDateTo)
        {
            var result = dbContext.Sessions
                .Where(q => q.UserId == userId);
            result = ApplyFiltering(startDateFrom, startDateTo, result);

            return await result.CountAsync();
        }

        private static IQueryable<Session> ApplyFiltering(DateTime? startDateFrom,
            DateTime? startDateTo, IQueryable<Session> result)
        {
            if (startDateFrom.HasValue)
                result = result
                    .Where(user => user.StartTime >= startDateFrom.Value);

            if (startDateTo.HasValue)
                result = result
                    .Where(user => user.StartTime <= startDateTo.Value);

            return result;
        }
    }
}
