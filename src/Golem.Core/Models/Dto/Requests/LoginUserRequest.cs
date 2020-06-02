using System.ComponentModel.DataAnnotations;

namespace Golem.Core.Models.Dto.Requests
{
    public class LoginUserRequest
    {
        /// <summary>
        ///     User email
        /// </summary>
        [Required]
        public string Email { get; set; }

        /// <summary>
        ///     User password
        /// </summary>
        [Required]
        public string Password { get; set; }
    }
}
