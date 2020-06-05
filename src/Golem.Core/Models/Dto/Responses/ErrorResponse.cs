using Golem.Core.Exceptions;

namespace Golem.Core.Models.Dto.Responses
{
    public class ErrorResponse
    {
        /// <summary>
        ///     Description for the errors
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        ///     Errors messages
        /// </summary>
        public Error[] Errors { get; set; }

        /// <summary>
        ///     Http response status code
        /// </summary>
        public int Status { get; set; }
    }
}
