namespace TestSystem.Models.TestAdministration
{
    public class AnswerModel
    {
        public int AnswerId { get; set; }
        public int QuestionId { get; set; }
        public string Name { get; set; }
        public int IsCorrect { get; set; }
    }
}