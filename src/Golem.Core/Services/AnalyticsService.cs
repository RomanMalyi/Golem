using System.Threading.Tasks;
using Golem.Core.Models.Dto.Responses;
using Golem.Data.PostgreSql.Repositories;

namespace Golem.Core.Services
{
    public class AnalyticsService
    {
        private readonly UserRepository userRepository;
        private readonly QueryRepository queryRepository;

        public AnalyticsService(UserRepository userRepository,
            QueryRepository queryRepository)
        {
            this.userRepository = userRepository;
            this.queryRepository = queryRepository;
        }

        public async Task<DashboardOverviewResponse> GetDashboardOverview()
        {
            return new DashboardOverviewResponse()
            {
                AverageNumberOfRequests = await userRepository.GetAverageNumberOfRequests(),
                NumberOfRequests = await queryRepository.GetCount(),
                NumberOfUsers = await userRepository.GetCount(),
            };
        }
    }
}
