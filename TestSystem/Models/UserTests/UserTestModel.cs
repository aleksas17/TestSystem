using System;
using System.Collections.Generic;

namespace TestSystem.Models.UserTests
{
    public class UserTestModel
    {
        /// <summary>
        /// User id
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Test id
        /// </summary>
        public int TestId { get; set; }

        /// <summary>
        /// User test status (active / finishted)
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// When did user start the test
        /// </summary>
        public DateTime? TestStart { get; set; }

        /// <summary>
        /// How long he took on doing the test
        /// </summary>
        public double Time { get; set; }

        /// <summary>
        /// User answers
        /// </summary>
        public List<UserAnswerModel> UserAnswers { get; set; }
    }
}