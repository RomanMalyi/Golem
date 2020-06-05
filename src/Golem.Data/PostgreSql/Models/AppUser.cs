using System;
using Microsoft.AspNetCore.Identity;

namespace Golem.Data.PostgreSql.Models
{
    public class AppUser: IdentityUser
    {
        /// <summary>
        ///     First name
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        ///     Last name
        /// </summary>
        public string LastName { get; set; }
        
        /// <summary>
        ///     Refresh token model id
        /// </summary>
        public Guid? RefreshTokenId { get; set; }

        /// <summary>
        ///     Refresh token model
        /// </summary>
        public RefreshToken RefreshToken { get; set; }
    }
}
