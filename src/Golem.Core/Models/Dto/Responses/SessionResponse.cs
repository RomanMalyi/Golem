using System;

namespace Golem.Core.Models.Dto.Responses
{
    public class SessionResponse
    {
        public Guid Id { get; set; }
        public UserResponse User { get; set; }
        public DateTimeOffset StartTime { get; set; }
        public DateTimeOffset EndTime { get; set; }
    }
}
