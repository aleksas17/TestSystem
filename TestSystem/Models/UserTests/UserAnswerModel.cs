using TestSystem.Models.TestAdministration;

namespace TestSystem.Models.UserTests
{
    public class UserAnswerModel
    {
        /// <summary>
        /// User answerd id
        /// </summary>
        public int UserAnswerId { get; set; }

        /// <summary>
        /// User test id
        /// </summary>
        public int UserTestId { get; set; }

        /// <summary>
        /// Test questions
        /// </summary>
        public QuestionModel QuestionModel { get; set; }
    }
}
