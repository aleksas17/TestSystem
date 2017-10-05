namespace TestSystem.ViewModels.UserTests
{
    public class TestViewModel
    {
        #region Public Properties

        /// <summary>
        /// Test name
        /// </summary>
        public string TestName { get; set; }

        /// <summary>
        /// Test id
        /// </summary>
        public int TestId { get; set; }

        /// <summary>
        /// Time left to do the test
        /// </summary>
        public long TimeLeft { get; set; }

        /// <summary>
        /// How many there are questions in test
        /// </summary>
        public int TotalQuestions { get; set; }

        /// <summary>
        /// How much questions there are left in test
        /// </summary>
        public int QuestionsLeft { get; set; }

        #endregion
    }
}