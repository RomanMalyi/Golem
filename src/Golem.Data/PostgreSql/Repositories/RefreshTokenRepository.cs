using System.Threading.Tasks;
using Golem.Data.PostgreSql.Models;

namespace Golem.Data.PostgreSql.Repositories
{
    public class RefreshTokenRepository
    {
        private readonly GolemContext petersrockContext;

        public RefreshTokenRepository(GolemContext dbContext)
        {
            petersrockContext = dbContext;
        }

        public async Task Delete(RefreshToken entity)
        {
            petersrockContext.RefreshTokens.Remove(entity);
            await petersrockContext.SaveChangesAsync();
        }
    }
}
