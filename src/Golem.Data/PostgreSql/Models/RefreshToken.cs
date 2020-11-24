using System;

namespace Golem.Data.PostgreSql.Models
{
    /// <summary>
    ///     Refresh token model
    /// </summary>
    public class RefreshToken
    {
        public Guid Id { get; set; }
        /// <summary>
        ///     User refresh token
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        ///     Refresh token expiration time in UTC with offset
        /// </summary>
        public DateTimeOffset TokenExpiration { get; set; }

        /// <summary>
        ///     User model id
        /// </summary>
        public string AppUserId { get; set; }

        /// <summary>
        ///     User model
        /// </summary>
        public AppUser User { get; set; }
    }
}
