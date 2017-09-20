using System.Collections.Generic;

namespace TestSystem.Models.UserTests
{
    public class TestModelView
    {
        //public QuestionModel QuestionModel { get; set; }
        //public List<AnswerModel> AnswerModels { get; set; }
        public UserAnswerModel UserAnswerModel { get; set; }
        public int TotalQuestions { get; set; }
        public  int QuestionsAnswered { get; set; }
    }
}