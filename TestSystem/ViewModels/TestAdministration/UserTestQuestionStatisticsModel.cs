using System.Collections.Generic;
using TestSystem.Models.TestAdministration;
using TestSystem.Models.UserTests;

namespace TestSystem.ViewModels.TestAdministration
{
    public class UserTestQuestionStatisticsModel
    {
        #region Public Properties
        public List<QuestionModel> QuestionModel { get; set; }
        public UserTestAnswersModel UserTestAnswer { get; set; }
        #endregion

    }
}