using System.Collections.Generic;

namespace TestSystem.Models.UserTests
{
    public class QuestionModel
    {
        public string Name;
        public List<AnswerModel> Answers { get; set; }
    }
  
}