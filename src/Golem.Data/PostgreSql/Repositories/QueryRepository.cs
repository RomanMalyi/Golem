using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Golem.Data.PostgreSql.Models;
using Microsoft.EntityFrameworkCore;

namespace Golem.Data.PostgreSql.Repositories
{
    public class QueryRepository
    {
        private readonly GolemContext dbContext;

        public QueryRepository(GolemContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Query> GetById(Guid id)
        {
            return await dbContext.Queries.FindAsync(id);
        }

        public async Task<IEnumerable<Query>> GetByUserId(Guid userid, int skip, int take)
        {
            return await dbContext.Queries
                .OrderBy(q => q.CreationDate)
                .Where(q => q.UserId == userid)
                .Skip(skip)
                .Take(take)
                .ToListAsync();
        }

        public async Task Create(Query entity)
        {
            dbContext.Queries.Add(entity);
            await dbContext.SaveChangesAsync();
        }

        public async Task Update(Query entity)
        {
            dbContext.Queries.Update(entity);
            await dbContext.SaveChangesAsync();
        }

        public async Task Delete(Query entity)
        {
            dbContext.Queries.Remove(entity);
            await dbContext.SaveChangesAsync();
        }
    }
}
