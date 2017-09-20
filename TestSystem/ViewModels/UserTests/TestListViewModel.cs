using System;

namespace TestSystem.ViewModels.UserTests
{
    public class TestListViewModel
    {
        public int UserTestId { get; set; }
        public string Name { get; set; }
        public DateTime? TestStart { get; set; }
        public TimeSpan Time { get; set; }
    }
}