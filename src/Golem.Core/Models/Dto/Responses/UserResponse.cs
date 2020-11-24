using System;

namespace Golem.Core.Models.Dto.Responses
{
    public class UserResponse
    {
        public Guid Id { get; set; }
        
        public int NumberOfVisits { get; set; }
        
        public int NumberOfRequests { get; set; }

        public DateTimeOffset LastVisitTime { get; set; }

        public DateTimeOffset FirstVisitTime { get; set; }

        public string Country { get; set; }
        
        public string Region { get; set; }
        
        public string City { get; set; }
        
        public string OperatingSystem { get; set; }
        
        public string Device { get; set; }

        public string UserAgent { get; set; }
    }
}
