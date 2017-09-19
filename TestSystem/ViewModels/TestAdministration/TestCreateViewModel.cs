using System;
using System.Collections.Generic;
using TestSystem.Models.TestAdministration;

namespace TestSystem.ViewModels.TestAdministration
{
    public class TestCreateViewModel
    {
        #region Public Propertys

        /// <summary>
        /// Main test 
        /// </summary>
        public string Name { get; set; }
        public string Language { get; set; }
        public int Duration { get; set; }
        public string Status { get; set; }
        public DateTime? TestEnd { get; set; }

        /// <summary>
        /// Test questions
        /// </summary>
        public List<QuestionModel> Questions { get; set; }

        #endregion
    }
}