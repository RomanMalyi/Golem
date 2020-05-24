using System;
using System.Collections.Generic;

namespace Golem.Data.PostgreSql.Models
{
    public class User
    {
        public User()
        {
            NumberOfVisits = 1;
            NumberOfRequests = 1;
        }

        public Guid Id { get; set; }
        public int NumberOfVisits { get; set; }
        
        public int NumberOfRequests { get; set; }

        public DateTimeOffset LastVisitTime { get; set; }

        public DateTimeOffset FirstVisitTime { get; set; }

        public string Country { get; set; }

        public string UserAgent { get; set; }
        
        public ICollection<Query> Queries { get; set; }
        public ICollection<Session> Sessions { get; set; }
    }
}
