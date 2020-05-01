using System;
using System.Threading.Tasks;
using Golem.Data.PostgreSql.Models;

namespace Golem.Data.PostgreSql.Repositories
{
    public class UserRepository
    {
        private readonly GolemContext dbContext;

        public UserRepository(GolemContext context)
        {
            dbContext = context;
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
    }
}
