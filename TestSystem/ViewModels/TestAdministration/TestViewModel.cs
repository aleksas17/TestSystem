using System;

namespace TestSystem.ViewModels.TestAdministration
{
    public class TestViewModel
    {
        #region Public Properties

        /// <summary>
        /// Test id
        /// </summary>
        public int TestId { get; set; }

        /// <summary>
        /// Test name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Test time, how long it will take (min)
        /// </summary>
        public int Duration { get; set; }

        /// <summary>
        /// Test end date
        /// </summary>
        public DateTime? TestEnd { get; set; }


        public string FinishedTests { get; set; }

        #endregion
    }
}