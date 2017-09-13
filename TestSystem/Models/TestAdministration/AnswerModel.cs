namespace TestSystem.Models.TestAdministration
{
    public class TestAnswer
    {
        public int AnswerId { get; set; }
        public int QuestionId { get; set; }
        public string Name { get; set; }
        public int Number { get; set; }
        public int IsCorrect { get; set; }
    }
}