using System.Collections.Generic;

namespace Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Role { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Position { get; set; }
        public string Group { get; set; }
        public string FirstName { get; set; }
        public string Lastname { get; set; }

        public virtual List<UserTest> UserTests { get; set; }
    }
}
