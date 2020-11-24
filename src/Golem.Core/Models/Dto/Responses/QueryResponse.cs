using System;

namespace Golem.Core.Models.Dto.Responses
{
    public class QueryResponse
    {
        public Guid Id { get; set; }
        public UserResponse User { get; set; }
        public string Path { get; set; }
        public string MethodType { get; set; }
        public string QueryString { get; set; }
        public string UserAgent { get; set; }
        public string IpAddress { get; set; }
        public DateTimeOffset CreationDate { get; set; }
    }
}
