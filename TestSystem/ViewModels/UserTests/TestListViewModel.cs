using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestSystem.ViewModels.UserTests
{
    public class TestListViewModel
    {
        public int UserTestId { get; set; }
        public string Name { get; set; }
        public DateTime? TestStart { get; set; }
        public double Time { get; set; }
    }
}