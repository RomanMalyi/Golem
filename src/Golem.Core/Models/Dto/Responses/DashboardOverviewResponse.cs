namespace Golem.Core.Models.Dto.Responses
{
    public class DashboardOverviewResponse
    {
        public int NumberOfUsers { get; set; }
        public int NumberOfRequests { get; set; }
        public double AverageNumberOfRequests { get; set; }
        public int NumberOfSessions { get; set; }
        public double AverageSessionDuration { get; set; }
        public double BounceRate { get; set; }
    }
}
