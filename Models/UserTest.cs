using System;
using System.Collections.Generic;

namespace Models
{
    public class UserTest
    {
        public int UserTestId { get; set; }
        public int UserId { get; set; }
        public int TestId { get; set; }
        public string Status { get; set; }
        public DateTime? TestStart { get; set; }
        public TimeSpan Time { get; set; }

        public virtual List<UserAnswer> UserAnswers { get; set; }
        public User User { get; set; }
        public Test Test { get; set; }
    }
}
