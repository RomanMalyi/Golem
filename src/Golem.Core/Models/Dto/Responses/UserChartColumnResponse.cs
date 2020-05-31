using System;

namespace Golem.Core.Models.Dto.Responses
{
    public class UserChartColumnResponse
    {
        public DateTimeOffset Date { get; set; }
        public int UsersNumber { get; set; }
        public int SessionsNumber { get; set; }
    }
}
