using System;
using System.Collections.Generic;
using TestSystem.Models.Account;
using TestSystem.Models.TestAdministration;

namespace TestSystem.ViewModels.TestAdministration
{
    public class AssignTestPartialViewModel
    {
        public string Status { get; set; }
        public string Name { get; set; }
        public DateTime? TestStart { get; set; }
        public IEnumerable<UserModel> UserModel { get; set; }
        public IEnumerable<TestModel> TestModel { get; set; }
    }
}