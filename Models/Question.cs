using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Question
    {
        public int QuestionId { get; set; }
        public int TestId { get; set; }
        [StringLength(500)]
        public string Name { get; set; }

        public virtual Test Test { get; set; }
        public virtual List<Answer> Answers { get; set; }
    }
}
