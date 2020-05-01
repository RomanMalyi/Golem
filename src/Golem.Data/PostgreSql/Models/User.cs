using System;
using System.Collections.Generic;

namespace Golem.Data.PostgreSql.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public int NumberOfVisits { get; set; }
        public ICollection<Query> Queries { get; set; }
    }
}
