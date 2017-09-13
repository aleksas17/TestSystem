using System;
using System.Collections.Generic;

namespace Models
{
    public class Test
    {
        public int TestId { get; set; }
        public string Name { get; set; }
        public string Language { get; set; }
        public int Duration { get; set; }
        public string Status { get; set; }
        public DateTime? TestEnd { get; set; }

        public virtual List<Question> Questions { get; set; }
        public virtual List<UserTest> UserTests { get; set; }
    }
}
