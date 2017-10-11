using System.Collections.Generic;
using TestSystem.Models.TestAdministration;

namespace TestSystem.ViewModels.UserTests
{
    public class TestListViewModel
    {
        #region Public Properties

        /// <summary>
        /// User test id
        /// </summary>
        public int UserTestId { get; set; }

        /// <summary>
        /// Tests with its information
        /// </summary>
        public TestModel Test { get; set; }

        /// <summary>
        /// User id
        /// </summary>
        public int UserId { get; set; }

        #endregion
    }
}