using System;
using System.Collections.Generic;
using TestSystem.Models.Account;
using TestSystem.Models.UserTests;

namespace TestSystem.ViewModels.TestAdministration
{
    public class AssignTestPartialViewModel
    {
        #region Public Propertys

        /// <summary>
        /// Test time.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Assigning test name.
        /// </summary>
        public string TestName { get; set; }

        /// <summary>
        /// Witch test is assigned.
        /// </summary>
        public int TestId { get; set; }

        /// <summary>
        /// What is the status for the test (active (defult on assign) / finished).
        /// </summary>
        public string Status { get; set; } = "active";

        /// <summary>
        /// Test start date (yy/MM/dd H:mm:ss).
        /// </summary>
        public DateTime? TestStart { get; set; }

        /// <summary>
        /// Test time.
        /// </summary>
        //public double Time { get; set; } = 2;

        public List<UserAnswerModel> UserAnswers { get; set; }

        /// <summary>
        /// Model for user list to select from.
        /// </summary>
        public IEnumerable<UserModel> UserModel { get; set; }

        #endregion
    }
}