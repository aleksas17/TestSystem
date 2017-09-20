using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Answer
    {
        public int AnswerId { get; set; }
        public int QuestionId { get; set; }
        [StringLength(140)]
        public string Name { get; set; }
        public int IsCorrect { get; set; } // Why not bool?

        public Question Question { get; set; }
    }
}
