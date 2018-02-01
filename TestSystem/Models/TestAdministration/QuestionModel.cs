using System.Collections.Generic;

namespace TestSystem.Models.TestAdministration
{
    public class QuestionModel
    { 
        /// <summary>
        /// Question id
        /// </summary>
        public int QuestionId { get; set; }

        /// <summary>
        /// Question name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Test questions choices with answers
        /// </summary>
        public List<AnswerModel> Answers { get; set; }
    }
}