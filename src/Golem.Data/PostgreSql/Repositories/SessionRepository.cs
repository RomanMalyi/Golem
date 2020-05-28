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

        public async Task<IEnumerable<Session>> GetByUserId(Guid userid, int skip, int take)
        {
            return await dbContext.Sessions
                .OrderBy(q => q.EndTime)
                .Where(q => q.UserId == userid)
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

        public async Task<int> GetCount()
        {
            return await dbContext.Sessions.CountAsync();
        }

        public async Task<int> GetCount(Guid userId)
        {
            return await dbContext.Sessions
                .Where(q => q.UserId == userId)
                .CountAsync();
        }
    }
}
