using System.Collections.Generic;
using TestSystem.Models.TestAdministration;
using TestSystem.Models.UserTests;

namespace TestSystem.ViewModels.TestAdministration
{
    public class TestStatisticsQuestionViewModel
    {

        public Dictionary<string, int> QuestionTotalGood { get; set; }
        public int TotalUserAnswers { get; set; }
        public List<QuestionModel> QuestionModel { get; set; }
        public UserTestAnswersModel UserTestAnswer { get; set; }
        public List<AnswerModel> Answer { get; set; }
        //public List<UserAnswerModel> UserAnswers { get; set; }
    }
}