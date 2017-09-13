using System;
using System.Collections.Generic;
using TestSystem.Models.TestAdministration;

namespace TestSystem.ViewModels.TestAdministration
{
    public class TestViewPartialViewModel
    {
        public int TestId { get; set; }
        public string Name { get; set; }
        public int Duration { get; set; }
        public DateTime? TestEnd { get; set; }
        public string FinishedTests { get; set; }

        public List<QuestionModel> Questions { get; set; }
        public List<UserTestStaticsModel> UserTests { get; set; }
    }
}