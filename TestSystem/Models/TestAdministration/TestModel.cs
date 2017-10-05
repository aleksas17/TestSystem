using System;
using System.Collections.Generic;
using TestSystem.Models.UserTests;

namespace TestSystem.Models.TestAdministration
{
    public class TestModel
    {
        /// <summary>
        /// Test id
        /// </summary>
        public int TestId { get; set; }

        /// <summary>
        /// Test name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Test language (LT / EN)
        /// </summary>
        public string Language { get; set; }

        /// <summary>
        /// Test time, how long it will take (min)
        /// </summary>
        public int Duration { get; set; }

        /// <summary>
        /// Test status (Active / Inactive)
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Test end date
        /// </summary>
        public DateTime? TestEnd { get; set; }
    }
}