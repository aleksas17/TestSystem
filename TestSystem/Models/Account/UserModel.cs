using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TestSystem.Models.UserTests;

namespace TestSystem.Models.Account
{
    public class UserModel
    {
        /// <summary>
        /// User id
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// User role (Admin / User)
        /// </summary>
        public string Role { get; set; }

        /// <summary>
        /// User username
        /// </summary>
        [Required(ErrorMessage = "Username can't be empty")]
        [RegularExpression("^(?![_.])(?!.*[_.]{2})[a-zA-Z0-9._]+(?<![_.])$", ErrorMessage = "Username not valid")]
        public string Username { get; set; }

        /// <summary>
        /// User password
        /// </summary>
        [DataType(DataType.Password, ErrorMessage = "Password not valid")]
        [RegularExpression("^(?![_.])(?!.*[_.]{2})[a-zA-Z0-9._]+(?<![_.])$", ErrorMessage = "Password not valid")]
        public string Password { get; set; }

        /// <summary>
        /// User repeat password
        /// </summary>
        [DataType(DataType.Password, ErrorMessage = "Password not valid")]
        [RegularExpression("^(?![_.])(?!.*[_.]{2})[a-zA-Z0-9._]+(?<![_.])$", ErrorMessage = "Password not valid")]
        public string RepeatPassword { get; set; }

        /// <summary>
        /// User group
        /// </summary>
        [Required(ErrorMessage = "Group can't be empty")]
        public string Group { get; set; }

        /// <summary>
        /// User postion (programer, QA, CEO,...)
        /// </summary>
        [Required(ErrorMessage = "Position can't be empty")]
        public string Position { get; set; }

        /// <summary>
        /// User firstname
        /// </summary>
        [Required(ErrorMessage = "Firstname can't be empty")]
        public string FirstName { get; set; }

        /// <summary>
        /// User lastname
        /// </summary>
        [Required(ErrorMessage = "Lastname can't be empty")]
        public string Lastname { get; set; }
    }
}