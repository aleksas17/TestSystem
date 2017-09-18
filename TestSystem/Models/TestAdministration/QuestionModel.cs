using System.Collections.Generic;
using TestSystem.Models.UserTests;

namespace TestSystem.Models.TestAdministration
{
    public class QuestionModel
    {
        public int QuestionId { get; set; }
        public int TestId { get; set; }
        public string Name;
    }
}