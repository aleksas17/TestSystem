using System;

namespace TestSystem.ViewModels.TestAdministration
{
    public class TestViewModel
    {
        public int TestId { get; set; }
        public string Name { get; set; }
        public int Duration { get; set; }
        public DateTime? TestEnd { get; set; }
        public string FinishedTests { get; set; }
    }
}