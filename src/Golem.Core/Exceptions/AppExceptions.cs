using System.Security.Claims;

namespace Golem.Core.Exceptions
{
    public static class AppExceptions
    {
        public static AppException EmailNotFound()
        {
            return new AppException(400, "Email not found.",
                new Error
                {
                    Message = "We could not find an account associated with that email address.",
                    RelatedTo = "email"
                });
        }

        public static AppException EmailNotConfirmed()
        {
            return new AppException(403, "Looks like email is not confirmed.",
                new Error
                {
                    Message = "Please confirm your email address.",
                    RelatedTo = "email"
                });
        }

        public static AppException EmailAlreadyTaken(string email)
        {
            return new AppException(400, "Email is already taken.",
                new Error {Message = $"Email: '{email}' is already taken.", RelatedTo = "email"});
        }

        public static AppException EmailNotSent()
        {
            return new AppException(500,
                "Confirmation email has not been sent. Make sure the email is correct or try later.", string.Empty);
        }

        public static AppException WrongPassword()
        {
            return new AppException(404, "Check your password.",
                new Error
                {
                    Message = "Wrong password. Try again or click Forgot password to reset it.",
                    RelatedTo = "password"
                });
        }

        public static AppException AlreadyLoggedOut()
        {
            return new AppException(400, string.Empty, "User already logged out.");
        }

        public static AppException UserNotFound(string identifier)
        {
            return new AppException(404, "User not found.", $"User with identifier '{identifier}' not found.");
        }

        public static AppException UserNotFound(ClaimsPrincipal claimsPrincipal)
        {
            return UserNotFound(claimsPrincipal?.FindFirstValue(ClaimTypes.NameIdentifier));
        }
    }
}
