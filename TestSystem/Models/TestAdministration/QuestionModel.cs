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
        /// Questions total answers that are good
        /// </summary>
        public int TotalAnswersGood { get; set; }

        /// <summary>
        /// Questions total answers that are bad
        /// </summary>
        public int TotalAnswersBad { get; set; }

        /// <summary>
        /// Test questions choices with answers
        /// </summary>
        public List<AnswerModel> Answers { get; set; }
    }
}