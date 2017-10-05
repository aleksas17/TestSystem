namespace TestSystem.Models.TestAdministration
{
    public class UserStatisticsModel
    {
        /// <summary>
        /// User firstname that did the test
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// User lastname that did the test
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// How much user got good answers
        /// </summary>
        public double Score { get; set; }

        /// <summary>
        /// In which postion it landed (1st, 2nd, 3nd,...)
        /// </summary>
        public string Position { get; set; }

        /// <summary>
        /// User group
        /// </summary>
        public string Group { get; set; }
    }
}