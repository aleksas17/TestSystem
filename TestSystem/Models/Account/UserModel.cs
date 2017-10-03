using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TestSystem.Models.UserTests;

namespace TestSystem.Models.Account
{
    public class UserModel
    {
        public int UserId { get; set; }
        public string Role { get; set; }
        [Required(ErrorMessage = "Username can't be empty")]
        [RegularExpression("^(?![_.])(?!.*[_.]{2})[a-zA-Z0-9._]+(?<![_.])$", ErrorMessage = "Username not valid")]
        public string Username { get; set; }
        [DataType(DataType.Password, ErrorMessage = "Password not valid")]
        [RegularExpression("^(?![_.])(?!.*[_.]{2})[a-zA-Z0-9._]+(?<![_.])$", ErrorMessage = "Password not valid")]
        public string Password { get; set; }
        [DataType(DataType.Password, ErrorMessage = "Password not valid")]
        [RegularExpression("^(?![_.])(?!.*[_.]{2})[a-zA-Z0-9._]+(?<![_.])$", ErrorMessage = "Password not valid")]
        public string RepeatPassword { get; set; }
        [Required(ErrorMessage = "Group can't be empty")]
        public string Group { get; set; }
        [Required(ErrorMessage = "Position can't be empty")]
        public string Position { get; set; }
        [Required(ErrorMessage = "Firstname can't be empty")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Lastname can't be empty")]
        public string Lastname { get; set; }

        public virtual List<UserTestModel> UserTests { get; set; }
    }
}