using System.ComponentModel.DataAnnotations;

namespace TestSystem.Models.Account
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Username can't be empty")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password can't be empty")]
        public string Password { get; set; }
    }
}