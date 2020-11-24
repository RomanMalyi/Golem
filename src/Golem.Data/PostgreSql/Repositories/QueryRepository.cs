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

        public async Task<IEnumerable<Query>> GetByUserId(Guid userid, DateTime? creationDateFrom,
            DateTime? creationDateTo, bool showEmpty, int skip, int take)
        {
            var result = dbContext.Queries
                .OrderByDescending(q => q.CreationDate)
                .Where(q => q.UserId == userid);
            result = ApplyFiltering(creationDateFrom, creationDateTo, showEmpty, result);

            return await result
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

        public async Task<int> GetCount()
        {
            return await dbContext.Queries.CountAsync();
        }

        public async Task<int> GetCount(Guid userId, DateTime? creationDateFrom,
            DateTime? creationDateTo, bool showEmpty)
        {
            var result = dbContext.Queries
                .Where(q => q.UserId == userId);
            result = ApplyFiltering(creationDateFrom, creationDateTo, showEmpty, result);

            return await result.CountAsync();
        }

        public async Task<int> GetCount(DateTime? creationDateFrom,
            DateTime? creationDateTo)
        {
            var result = dbContext.Queries.AsQueryable();
            result = ApplyFiltering(creationDateFrom, creationDateTo, true, result);

            return await result.CountAsync();
        }

        private static IQueryable<Query> ApplyFiltering(DateTime? creationDateFrom,
            DateTime? creationDateTo, bool showEmpty, IQueryable<Query> result)
        {
            if (!showEmpty)
                result = result
                    .Where(query => query.QueryString != "" && query.QueryString != "searchTerm=");

            if (creationDateFrom.HasValue)
                result = result
                    .Where(query => query.CreationDate >= creationDateFrom.Value);

            if (creationDateTo.HasValue)
                result = result
                    .Where(query => query.CreationDate <= creationDateTo.Value);

            return result;
        }
    }
}
