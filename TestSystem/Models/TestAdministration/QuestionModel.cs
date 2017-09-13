using System.Collections.Generic;
using TestSystem.Models.UserTests;

namespace TestSystem.Models.TestAdministration
{
    public class QuestionModel
    {
        public string Name { get; set; }
        public string Choices { get; set; }

        public List<AnswerModel> Answers { get; set; }
    }
}