using TestSystem.Models.TestAdministration;

namespace TestSystem.Models.UserTests
{
    public class UserAnswerModel
    {
        public int UserAnswerId { get; set; }
        public int UserTestId { get; set; }
        public int AnswerId { get; set; }
        public QuestionModel QuestionModel { get; set; }
        // Modified if somthing dosen't work
        public AnswerModel Answer { get; set; }
    }
}