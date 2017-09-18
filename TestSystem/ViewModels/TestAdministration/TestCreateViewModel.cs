using System.Collections.Generic;
using TestSystem.Models.TestAdministration;

namespace TestSystem.ViewModels.TestAdministration
{
    public class TestCreateViewModel
    {
        #region Public Propertys

        /// <summary>
        /// Main test 
        /// </summary>
        public TestModel TestModel { get; set; }

        /// <summary>
        /// Test questions
        /// </summary>
        public IEnumerable<QuestionModel> QuestionModel { get; set; }

        /// <summary>
        /// Test questions choices with answers
        /// </summary>
        public IEnumerable<AnswerModel> AnswerModel { get; set; }

        #endregion
    }
}