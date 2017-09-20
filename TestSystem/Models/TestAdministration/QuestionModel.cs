using System.Collections.Generic;

namespace TestSystem.Models.TestAdministration
{
    public class QuestionModel
    {
        /// <summary>
        /// Question
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Test questions choices with answers
        /// </summary>
        public List<AnswerModel> Answers { get; set; }
    }
}