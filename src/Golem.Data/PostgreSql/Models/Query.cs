using System;

namespace Golem.Data.PostgreSql.Models
{
    public class Query
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public string Text { get; set; }
        public DateTimeOffset CreationDate { get; set; }
    }
}
