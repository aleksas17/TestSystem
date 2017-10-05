using System.ComponentModel.DataAnnotations;

namespace TestSystem.Models.Account
{
    public class LoginModel
    {
        /// <summary>
        /// User username
        /// </summary>
        [Required(ErrorMessage = "Username can't be empty")]
        public string Username { get; set; }

        /// <summary>
        /// User password
        /// </summary>
        [Required(ErrorMessage = "Password can't be empty")]
        public string Password { get; set; }
    }
}