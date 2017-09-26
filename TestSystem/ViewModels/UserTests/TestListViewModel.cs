using System.Collections.Generic;
using TestSystem.Models.TestAdministration;

namespace TestSystem.ViewModels.UserTests
{
    public class TestListViewModel
    {
        public int UserTestId { get; set; }
        public TestModel Test { get; set; }
    }
}