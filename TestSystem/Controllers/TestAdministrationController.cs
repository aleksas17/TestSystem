using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Data;
using Models;
using TestSystem.Models.TestAdministration;
using PagedList;
using TestSystem.ViewModels.TestAdministration;
using TestSystem.Models.Account;

namespace TestSystem.Controllers
{
    public class TestAdministrationController : Controller
    {
        /// <summary>
        /// Deletes test from list.
        /// </summary>
        /// <param name="id">Test id</param>
        /// <returns>Refrested list</returns>
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteRecord(int id)
        {
            using (var uow = new UnitOfWork())
            {
                var test = uow.TestRepository.Get(id);
                uow.TestRepository.Remove(test);
                uow.Commit();
            }
            return RedirectToAction("TestList", "TestAdministration");
        }

        /// <summary>
        /// Create test popup.
        /// </summary>
        /// <returns>Create test partial view</returns>
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult CreateTest()
        {
            return PartialView("TestCreatePartial");
        }

        /// <summary>
        /// Creating test by inserting testCreateViewModel data to DB
        /// </summary>
        /// <param name="testCreateViewModel">Test values</param>
        /// <returns>Refresh curent page</returns>
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult CreateTest(TestCreateViewModel testCreateViewModel)
        {
            var test = Mapper.Map<Test>(testCreateViewModel);
            using (var uow = new UnitOfWork()) {
                uow.TestRepository.Add(test);
                uow.Commit();
            }
            return JavaScript("location.reload(true)");
        }

        /// <summary>
        /// Get user list for assign popup.
        /// </summary>
        /// <param name="id">Test id</param>
        /// <returns>Partial view with viewmodel</returns>
        [Authorize(Roles ="Admin")]
        [HttpGet]
        public ActionResult AssignTest(int? id)
        {
            var assignTestPartialViewModel = new AssignTestPartialViewModel();
            using (var uow = new UnitOfWork())
            {
                // Geting user list.
                var userModels = new List<UserModel>();
                IEnumerable<User> users;
                users = uow.UserRepository.GetAll();
                foreach (var user in users)
                {
                    var userModel = Mapper.Map<UserModel>(user);
                    userModels.Add(userModel);
                }
                assignTestPartialViewModel.UserModel = userModels;
                // Geting test name and id.
                var test = uow.TestRepository.Get(id);
                assignTestPartialViewModel.TestName = test.Name;
                assignTestPartialViewModel.TestId = test.TestId;
            }
            return PartialView("AssignTestPartial", assignTestPartialViewModel);
        }

        /// <summary>
        /// Assign test to selected user.
        /// </summary>
        /// <param name="assignTestPartialViewModel">Test id, test time and start date</param>
        /// <param name="usersIds">Selected users</param>
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult AssignTestPartial(AssignTestPartialViewModel assignTestPartialViewModel, int[] usersIds)
        {
            if (!ModelState.IsValid || usersIds == null) return new EmptyResult();
            using (var uow = new UnitOfWork())
            {
                var questions = uow.QuestionRepository.GetQuestionsByTestId(assignTestPartialViewModel.TestId);
                foreach (var userId in usersIds)
                {
                    // Adding to userTest table.
                    assignTestPartialViewModel.UserId = userId;
                    var usertest = Mapper.Map<UserTest>(assignTestPartialViewModel);
                    uow.UserTestRepository.Add(usertest);
                    // Populating userAnswer table with question and test id's
                    usertest.UserAnswers = Mapper.Map<List<UserAnswer>>(questions);
                }
                uow.Commit();
            }
            return JavaScript("location.reload(true)");
        }

        /// <summary>
        /// Sortin test list and searching in test list
        /// </summary>
        /// <param name="sortOrder">What sort order is right now</param>
        /// <param name="currentFilter">what filter we selected</param>
        /// <param name="searchString">Test name</param>
        /// <param name="page">In witch test list page we are searchin and filtering</param>
        /// <returns>Sorted list / Searchted item</returns>
        [Authorize(Roles = "Admin")]
        public ActionResult TestList(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = string.IsNullOrEmpty(sortOrder) ? "username_desc" : "";
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var testModels = new List<TestModel>();
            using (var uow = new UnitOfWork())
            {
                IEnumerable<Test> tests;
                if (!string.IsNullOrEmpty(searchString))
                {
                    tests = uow.TestRepository.SearchByKeyword(searchString);
                }
                else
                {
                    tests = uow.TestRepository.GetAll();
                }
                foreach (var test in tests)
                {
                    var testModel = Mapper.Map<TestModel>(test);
                    testModels.Add(testModel);
                }
            }
            switch (sortOrder)
            {
                // Order by descending test name
                case "name_desc":
                    testModels = testModels.OrderByDescending(s => s.Name).ToList();
                break;
            }
            var pageSize = 10;
            var pageNumber = (page ?? 1);
            return View(testModels.ToPagedList(pageNumber, pageSize));
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult UsersStatistics()
        {
                return PartialView("TestStatisticsPartial");
        }

        /// <summary>
        /// Get all user scors for test
        /// </summary>
        /// <param name="testId">Witch test scors we want</param>
        /// <param name="sortOrder"></param>
        /// <param name="currentFilter"></param>
        /// <param name="searchString"></param>
        /// <param name="page"></param>
        /// <returns>List of user statistic for test</returns>
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult TestStatisticsUsers(int testId, string sortOrder, string currentFilter, string searchString, int? page)
        {
            using (var uow = new UnitOfWork())
            {
                var userTests = uow.UserTestRepository.GetUserTestsByTestId(testId);
                var usersScore = Mapper.Map<List<UsersScoresViewModel>>(userTests);

                foreach (var userScore in usersScore)
                {
                    userScore.Position = usersScore.Count(a => a.Score > userScore.Score) + 1;
                    userScore.Position += usersScore.Count(a => a.Score == userScore.Score && a.Time < userScore.Time);

                }
                var pageSize = 7;
                var pageNumber = (page ?? 1);
                return PartialView("TestStatisticsUsersPartial", usersScore.ToPagedList(pageNumber, pageSize));
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult TestStatisticsQuestion()
        {
            return PartialView("TestStatisticsQuestionPartial");
        }
    }

    //    public ActionResult Test()
    //    {
    //        using (var uow = new UnitOfWork())
    //        {
    //            var tests = uow.TestRepository.GetAllActivatedTests();
    //            var testModels = Mapper.Map<List<TestViewModel>>(tests);
    //            return View(testModels);
    //        }
    //    }

    //    public ActionResult TestTemplates()
    //    {
    //        using (var uow = new UnitOfWork())
    //        {
    //            var tests = uow.TestRepository.GetAllByStatus("Inactive");
    //            var testModels = Mapper.Map<List<TestTemplatesViewModel>>(tests);
    //            return View(testModels);
    //        }
    //    }

        
    //    public ActionResult TestTemplatesPartial(int testId)
    //    {
    //        using (var uow = new UnitOfWork())
    //        {
    //            var test = uow.TestRepository.GetTestById(testId);
    //            var testModel = Mapper.Map<TestTemplatesPartialViewModel>(test);
    //            return View(testModel);
    //        }
    //    }

    //    [HttpPost]
    //    public ActionResult _TestProperties(string testName)
    //    {
    //        using (var uow = new UnitOfWork())
    //        {
    //            var test = uow.TestRepository.GetTestByName(testName);
    //            var testModel = Mapper.Map<TestViewModel>(test);
    //            testModel.FinishedTests = test.UserTests.Count(a => a.Status == "Finished")+"/" +test.UserTests.Count;

    //            return PartialView();
    //        }
    //    }

    //    [HttpPost]
    //    public ActionResult _TestStatistics()
    //    {
            
            
    //        return PartialView();
    //    }

    //    [HttpPost]
    //    public ActionResult _UserStatistics(string testName)
    //    {
    //        using (var uow = new UnitOfWork())
    //        {
    //            var test = uow.UserTestRepository.GetUserTestsByTestName(testName);
                
    //            var model =Mapper.Map<List<UserStatisticsModel>>(test);
                
                
    //            return View(model.ToPagedList(1,8));
    //        }
    //    }

    //    [HttpPost]
    //    public ActionResult _QuestionStatistics()
    //    {
    //        return null;
    //    }


    //    [HttpPost]
    //    public ActionResult _TestTemplateProperties(string testName)
    //    {
    //        using (var uow = new UnitOfWork())
    //        {
    //            var test = uow.TestRepository.GetTestByName(testName);
    //            var testModel = Mapper.Map<TestTemplatesViewModel>(test);
    //            return PartialView(testModel);
    //        }
    //    }

    //    [HttpPost]
    //    public ActionResult Upload(HttpPostedFileBase upload, int testId)
    //    {
    //        if (ModelState.IsValid)
    //        {
    //            if (upload != null && upload.ContentLength > 0)
    //            {
    //                if (upload.FileName.EndsWith(".csv"))
    //                {
    //                    var stream = upload.InputStream;
    //                    using (var reader = new StreamReader(stream))
    //                    {
    //                        var questions = new List<Question>();
    //                        while (!reader.EndOfStream)
    //                        {
    //                            var line = reader.ReadLine().Split(';',',');
    //                            var question = new Question()
    //                            {
    //                                Name = line[0],
    //                                TestId = testId
    //                            };

    //                            var answers = new List<Answer>();
    //                            for (var a = 2; a < line.Length;a++)
    //                            {
    //                                var answer = new Answer
    //                                {
    //                                    Name = line[a],
    //                                    IsCorrect = (a-1) == Convert.ToInt32(line[1]) ? 1 : 0,
    //                                };

    //                                answers.Add(answer);
    //                            }
    //                            question.Answers = answers;
    //                            questions.Add(question);
    //                        }
    //                        using (var uow = new UnitOfWork())
    //                        {
    //                            uow.QuestionRepository.AddRange(questions);
    //                            uow.Commit();
    //                        }
    //                    }
    //                }
    //            }
    //        }
    //        return RedirectToAction("TestTemplates","TestAdministration");
    //    }

    //    [HttpPost]
    //    public ActionResult _TestTemplateQuestions(string testName)
    //    {
    //        using (var uow = new UnitOfWork())
    //        {
    //            var questions = uow.QuestionRepository.GetQuestionsByTestName(testName);
    //            var questionModels = Mapper.Map<IEnumerable<Question>, IEnumerable<QuestionModel>>(questions);

    //            return PartialView(questionModels);
    //        }
    //    }

    //    [HttpGet]
    //    public ActionResult AddTest()
    //    {
    //        return PartialView("_TestCreateDialog");
    //    }

    //    [HttpPost]
    //    public ActionResult AddTest(TestTemplatesViewModel testModel)
    //    {
    //        if (ModelState.IsValid)
    //        {
    //            using (var uow = new UnitOfWork())
    //            {
    //                var test = new Test()
    //                {
    //                    Name = testModel.Name,
    //                    Status = "Inactive",
    //                    Language = "temp"
    //                };
    //                uow.TestRepository.Add(test);
    //                uow.Commit();
    //            }
    //            return JavaScript("location.reload(true)");
    //        }
    //        return RedirectToAction("AddTest",testModel);
    //    }

    //    public ActionResult ActivateTest(int? id)
    //    {

    //        return View("_TestActivateDialog");
    //    }

    //    [HttpPost]
    //    public ActionResult ActivateTest(int testId)
    //    {
    //        if (ModelState.IsValid)
    //        {
    //            using (var uow = new UnitOfWork())
    //            {
    //                var test = uow.TestRepository.GetTestById(testId);
    //                test.Status = "active";
    //                test.TestEnd = DateTime.Now.AddHours(2);
    //                var users = uow.UserRepository.GetAll();
    //                var userTests = new List<UserTest>();
    //                foreach (var user in users)
    //                {
    //                    var userTest = new UserTest
    //                    {
    //                        Status = "Inactive",
    //                        UserId = user.UserId,
    //                        TestId = test.TestId,
    //                        UserAnswers = Mapper.Map<List<UserAnswer>>(test.Questions)
    //                    };

    //                    userTests.Add(userTest);
    //                }
    //                uow.UserTestRepository.AddRange(userTests);
    //                uow.Commit();
    //            }
    //        }
    //        return null;
    //    }
    //}
}