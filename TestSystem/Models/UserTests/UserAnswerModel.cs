using TestSystem.Models.TestAdministration;

namespace TestSystem.Models.UserTests
{
    public class UserAnswerModel
    {
        public int UserAnswerId { get; set; }
        public int UserTestId { get; set; }
        public QuestionModel QuestionModel { get; set; }
    }
}