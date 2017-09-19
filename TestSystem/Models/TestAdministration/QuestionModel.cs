using System.Collections.Generic;

namespace TestSystem.Models.TestAdministration
{
    public class QuestionModel
    {
        //public int QuestionId { get; set; }
        //public int TestId { get; set; }
        public string Name { get; set; }

        /// <summary>
        /// Test questions choices with answers
        /// </summary>
        public List<AnswerModel> Answers { get; set; }
    }
}