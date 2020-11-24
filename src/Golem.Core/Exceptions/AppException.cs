using System;
using System.Linq;

namespace Golem.Core.Exceptions
{
    public class AppException : Exception
    {
        private const string GeneralRelatedTo = "model";

        public AppException(int statusCode,
            string description,
            Error[] errors) : base(description)
        {
            Code = statusCode;
            Description = description;
            Errors = errors;
        }

        public AppException(int statusCode,
            string description,
            Error error) : base(description)
        {
            Code = statusCode;
            Description = description;
            Errors = new[] {error};
        }

        public AppException(int statusCode,
            string description,
            string[] errors) : base(description)
        {
            Code = statusCode;
            Description = description;
            Errors = errors.Select(e => new Error {Message = e, RelatedTo = GeneralRelatedTo}).ToArray();
        }

        public AppException(int statusCode,
            string description,
            string error) : base(description)
        {
            Code = statusCode;
            Description = description;
            Errors = new[] {new Error {Message = error, RelatedTo = GeneralRelatedTo}};
        }

        public int Code { get; }

        public string Description { get; }

        public Error[] Errors { get; }
    }
}
