using System.ComponentModel.DataAnnotations;

namespace Golem.Core.Models.Dto.Requests
{
    public class RefreshAccessTokenRequest
    {
        /// <summary>
        ///     User access token
        /// </summary>
        [Required]
        public string AccessToken { get; set; }

        /// <summary>
        ///     User refresh token
        /// </summary>
        [Required]
        public string RefreshToken { get; set; }
    }
}
