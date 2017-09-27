using TestSystem.Models.TestAdministration;
using TestSystem.Models.UserTests;

namespace TestSystem.ViewModels.UserTests
{
    public class TestPartialViewModel
    {
        public int UserAnswerId { get; set; }
        public int UserTestId { get; set; }
        public QuestionModel Question { get; set; }
    }
}