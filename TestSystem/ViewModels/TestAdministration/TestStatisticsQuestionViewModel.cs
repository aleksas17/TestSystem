using System.Collections.Generic;
using TestSystem.Models.TestAdministration;

namespace TestSystem.ViewModels.TestAdministration
{
    public class TestStatisticsQuestionViewModel
    {
        public string Name { get; set; }

        public Dictionary<int?, int> QuestionTotalGood { get; set; }
    }
}