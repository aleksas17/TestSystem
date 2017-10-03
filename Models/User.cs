using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class User
    {
        public int UserId { get; set; }
        [StringLength(140)]
        public string Role { get; set; }
        [StringLength(140)]
        public string Username { get; set; }
        [StringLength(140)]
        public string Password { get; set; }
        [StringLength(140)]
        public string Position { get; set; }
        [StringLength(140)]
        public string Group { get; set; }
        [StringLength(140)]
        public string FirstName { get; set; }
        [StringLength(140)]
        public string Lastname { get; set; }

        public virtual List<UserTest> UserTests { get; set; }
    }
}
