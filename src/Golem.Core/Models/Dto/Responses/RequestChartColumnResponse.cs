using System;

namespace Golem.Core.Models.Dto.Responses
{
    public class RequestChartColumnResponse
    {
        public DateTimeOffset Date { get; set; }
        public int RequestsNumber { get; set; }
    }
}
