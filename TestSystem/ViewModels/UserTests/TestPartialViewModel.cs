using TestSystem.Models.TestAdministration;
using TestSystem.Models.UserTests;

namespace TestSystem.ViewModels.UserTests
{
    public class TestPartialViewModel
    {
        #region Public Properties

        /// <summary>
        /// User slected answer id
        /// </summary>
        public int UserAnswerId { get; set; }

        /// <summary>
        /// User test id that he is doing
        /// </summary>
        public int UserTestId { get; set; }

        /// <summary>
        /// test questions
        /// </summary>
        public QuestionModel QuestionModel { get; set; }

        #endregion
    }
}