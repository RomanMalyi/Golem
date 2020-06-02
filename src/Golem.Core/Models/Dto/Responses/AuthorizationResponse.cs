using System;

namespace Golem.Core.Models.Dto.Responses
{
    public class AuthorizationResponse
    {
        /// <summary>
        ///     User access token
        /// </summary>
        public string AccessToken { get; set; }

        /// <summary>
        ///     Access token expiration time in UTC with offset
        /// </summary>
        public DateTimeOffset AccessTokenExpirationDateTimeOffset { get; set; }

        /// <summary>
        ///     User refresh token
        /// </summary>
        public string RefreshToken { get; set; }
    }
}
