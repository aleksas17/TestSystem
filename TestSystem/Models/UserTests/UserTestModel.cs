using System;
using System.Collections.Generic;

namespace TestSystem.Models.UserTests
{
    public class UserTestModel
    {
        public int UserId { get; set; }
        public int TestId { get; set; }
        public string Status { get; set; }
        public DateTime? TestStart { get; set; }
        public double Time { get; set; }

        public List<UserAnswerModel> UserAnswers { get; set; }
    }
}