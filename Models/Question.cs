using System.Collections.Generic;

namespace Models
{
    public class Question
    {
        public int QuestionId { get; set; }
        public int TestId { get; set; }
        public string Name { get; set; }

        public virtual Test Test { get; set; }
        public virtual List<Answer> Answers { get; set; }
    }
}
