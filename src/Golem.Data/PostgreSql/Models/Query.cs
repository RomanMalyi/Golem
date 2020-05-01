using System;

namespace Golem.Data.PostgreSql.Models
{
    public class Query
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public string Path { get; set; }
        public string MethodType { get; set; }
        public string QueryString { get; set; }//TODO: change to diff data types with body?
        public string UserAgent { get; set; }
        public string IpAddress { get; set; }
        public DateTimeOffset CreationDate { get; set; }
    }
}
