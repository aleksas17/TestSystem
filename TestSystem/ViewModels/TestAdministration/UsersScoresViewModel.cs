using System;

namespace TestSystem.ViewModels.TestAdministration
{
    public class UsersScoresViewModel
    {
        #region Public Properties

        /// <summary>
        /// User FirstName
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// User LastName
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// User test score 
        /// </summary>
        public int Score { get; set; }

        /// <summary>
        /// how much time user took to do test
        /// </summary>
        public TimeSpan Time { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Position { get; set; }

        #endregion
    }
}