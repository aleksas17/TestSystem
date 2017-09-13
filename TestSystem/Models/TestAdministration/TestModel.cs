using System;

namespace TestSystem.Models.TestAdministration
{
    public class TestModel
    {
        public int TestId { get; set; }
        public string Name { get; set; }
        public string Language { get; set; }
        public int Duration { get; set; }
        public string Status { get; set; }
        public DateTime? TestEnd { get; set; }
    }
}