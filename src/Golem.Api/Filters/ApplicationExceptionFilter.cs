using System.Linq;
using Golem.Core.Exceptions;
using Golem.Core.Models.Dto.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace Golem.Api.Filters
{
    public class ApplicationExceptionFilter : ExceptionFilterAttribute
    {
        private const string InternalServerErrorMessage = "Looks like something went wrong...";

        private readonly ILogger<ApplicationExceptionFilter> logger;

        public ApplicationExceptionFilter(ILogger<ApplicationExceptionFilter> logger)
        {
            this.logger = logger;
        }

        public override void OnException(ExceptionContext context)
        {
            var result = new ErrorResponse();

            if (context.Exception is AppException exception)
            {
                context.HttpContext.Response.StatusCode = exception.Code;
                result.Description = exception.Description;
                result.Errors = exception.Errors;
                result.Status = exception.Code;

                logger.LogWarning(
                    $"Description: {result.Description}\r\n Messages: {string.Join(",", result.Errors.Select(e => $"{e.RelatedTo}: {e.Message}"))}");
            }
            else
            {
                result.Status = 500;
                result.Description = context.Exception.Message;
                result.Errors = new[] {new Error {Message = InternalServerErrorMessage}};
                context.HttpContext.Response.StatusCode = result.Status;

                logger.LogError($"{context.Exception}");
            }

            context.Result = new JsonResult(result);
        }
    }
}
