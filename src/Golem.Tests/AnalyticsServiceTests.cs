using System;
using System.Threading.Tasks;
using FluentAssertions;
using Golem.Core.Services;
using Golem.Data.PostgreSql.Repositories;
using Moq;
using Xunit;

namespace Golem.Tests
{
    public class AnalyticsServiceTests
    {
        private readonly AnalyticsService analyticsService;

        public AnalyticsServiceTests()
        {
            var analyticUserRepository = new Mock<AnalyticUserRepository>(MockBehavior.Loose);
            analyticUserRepository.Setup(r => r.GetCount(It.IsAny<DateTime?>(), It.IsAny<DateTime?>()))
                .Returns(Task.FromResult(16));
            analyticUserRepository.Setup(repo => repo.GetAverageNumberOfRequests())
                .Returns(Task.FromResult(12.5));
            var queryRepository = new Mock<QueryRepository>();
            queryRepository.Setup(repo => repo.GetCount())
                .Returns(Task.FromResult(186));
            var sessionRepository = new Mock<SessionRepository>();
            sessionRepository.Setup(repo => repo.GetCount())
                .Returns(Task.FromResult(145));

            analyticsService = new AnalyticsService(analyticUserRepository.Object, queryRepository.Object,
                sessionRepository.Object, null);
        }
        
        [Fact]
        public async void ShouldGetDashboardOverview()
        {
            var result = await analyticsService.GetDashboardOverview();
            result.Should().NotBeNull();
        }
        
        [Fact]
        public async void ShouldGetGetUsersChartInfo()
        {
            var result = await analyticsService.GetUsersChartInfo();
            result.Should().NotBeNull();
        }
        
        [Fact]
        public async void ShouldGetRequestsChartInfo()
        {
            var result = await analyticsService.GetRequestsChartInfo();
            result.Should().NotBeNull();
        }
    }
}
