namespace Models
{
    public class UserAnswer
    {
        public int UserAnswerId { get; set; }
        public int UserTestId { get; set; }
        public int? QuestionId { get; set; }
        public int? AnswerId { get; set; }

        public UserTest UserTest { get; set; }
        public Question Question { get; set; }
        public Answer Answer { get; set; }
    }
}
