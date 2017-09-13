using System;

namespace TestSystem.Models.UserTests
{
    public class UserTestModel
    {
        public int UserTestId { get; set; }
        public string Status { get; set; }
        public string Name { get; set; }
        public DateTime? TestStart { get; set; }
    }
}