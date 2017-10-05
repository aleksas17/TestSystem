using System.Collections.Generic;
using TestSystem.Models.TestAdministration;
using TestSystem.Models.UserTests;

namespace TestSystem.ViewModels.TestAdministration
{
    public class TestStatisticsQuestionViewModel
    {
        #region Public Properties

        /// <summary>
        /// How much there are questions answered good
        /// </summary>
        public Dictionary<string, int> QuestionTotalGood { get; set; }

        /// <summary>
        /// How much user did the test
        /// </summary>
        public int TotalUserAnswers { get; set; }

        /// <summary>
        /// Test questions
        /// </summary>
        public List<QuestionModel> QuestionModel { get; set; }

        /// <summary>
        /// User answers for test
        /// </summary>
        public UserTestAnswersModel UserTestAnswer { get; set; }

        /// <summary>
        /// Question answers
        /// </summary>
        public List<AnswerModel> Answer { get; set; }

        #endregion
    }
}