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
using System.Web;
using System.IO;
using System;
using System.Text;

namespace TestSystem.Controllers
{
    [Authorize(Roles = "Admin")]
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
        [HttpGet]
        public ActionResult AssignTest(int id)
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
                    var userTest = user.UserTests.FirstOrDefault(a => a.TestId == id);
                    var status = userTest != null ? userTest.Status : string.Empty;
                    if (userTest==null ||status == "finished")
                    {
                        var userModel = Mapper.Map<UserModel>(user);
                        userModels.Add(userModel);
                    }
                    
                }
                assignTestPartialViewModel.Users = userModels;
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
        /// <param name="page">In which page we are right now</param>
        /// <returns>Sorted list / Searchted item</returns>
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

        /// <summary>
        /// Get user statistics partial view
        /// </summary>
        /// <returns>partial view</returns>
        [HttpGet]
        public ActionResult UsersStatistics()
        {
            return PartialView("TestStatisticsPartial");
        }

        /// <summary>
        /// Get all user scors from test
        /// </summary>
        /// <param name="testId">Witch test scors we want</param>
        /// <param name="sortOrder">Not inplamented</param>
        /// <param name="currentFilter">Not inplamented</param>
        /// <param name="searchString">Not inplamented</param>
        /// <param name="page">Whitch page</param>
        /// <returns>List of user statistic for test</returns>
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
                return PartialView("TestStatisticsUsersPartial", usersScore.OrderBy(a=>a.Position).ToPagedList(pageNumber, pageSize));
            }
        }

        /// <summary>
        /// Get all questions answers from test
        /// </summary>
        /// <param name="testId">Witch test questions we want</param>
        /// <param name="sortOrder">Not inplamented</param>
        /// <param name="currentFilter">Not inplamented</param>
        /// <param name="searchString">Not inplamented</param>
        /// <param name="page">Whitch page</param>
        /// <returns>List of qeustions and user answers</returns>
        [HttpGet]
        public ActionResult TestStatisticsQuestion(int testId, string sortOrder, string currentFilter, string searchString, int? page)
        {
            using (var uow = new UnitOfWork())
            {
                var userTestsAnswers = uow.UserTestRepository.GetUserAnswersByTestId(testId);
                var userAnswers = Mapper.Map<List<TestStatisticsQuestionViewModel>>(userTestsAnswers);
                var answers = uow.UserAnswerRepository.GetUserAnswersByTestId(testId).GroupBy(a => a.Question.Name);
                var mydictionary = new Dictionary<string, int>();
                var totalAnswers = 0;
                foreach (var a in answers)
                {
                    var count = a.Where(x => x.AnswerId != null).Where(x => x.Answer.IsCorrect == 1).Count();
                    mydictionary.Add(a.Key, value: count);
                }
                foreach (var user in userAnswers.Select(b => b.UserTestAnswer))
                {
                    totalAnswers += 1;
                }
                if (userAnswers.Count > 0)
                {
                    for (int i = 0; i < userAnswers.Count(); i++)
                    {
                        userAnswers[i].QuestionTotalGood = mydictionary;
                        userAnswers[i].TotalUserAnswers = totalAnswers;
                    }
                }
                var pageSize = 7;
                var pageNumber = (page ?? 1);
                return PartialView("TestStatisticsQuestionPartial", userAnswers.ToPagedList(pageNumber, pageSize));
            }
        }
    
        /// <summary>
        /// CSV file uploader for test
        /// </summary>
        /// <param name="upload">csv file</param>
        /// <returns>Create test from csv file and realod page</returns>
        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                if (upload != null && upload.ContentLength > 0)
                {
                    if (upload.FileName.EndsWith(".csv"))
                    {
                        var stream = upload.InputStream;
                        using (var reader = new StreamReader(stream, Encoding.Default, true))
                        {
                            string TestheaderLine = reader.ReadLine();
                            var line = reader.ReadLine().Split(';');
                            var test = new Test()
                            {
                                Name = line[0],
                                Duration = Convert.ToInt32(line[1]),
                                Language = "LT",
                                Status = "Active"
                            };
                            int testId;
                            using (var uow = new UnitOfWork())
                            {
                                uow.TestRepository.Add(test);
                                uow.Commit();
                                testId = uow.TestRepository.GetTestByName(test.Name).TestId;
                            }
                            reader.ReadLine();
                            var questions = new List<Question>();
                            while (!reader.EndOfStream)
                            {
                                line = reader.ReadLine().Split(';');
                                var question = new Question()
                                {
                                    Name = line[0],
                                    TestId = testId
                                };

                                var answers = new List<Answer>();
                                for (var a = 2; a < line.Length; a++)
                                {
                                    var answer = new Answer
                                    {
                                        Name = line[a],
                                        IsCorrect = (a - 1) == Convert.ToInt32(line[1]) ? 1 : 0,
                                    };

                                    answers.Add(answer);
                                }
                                question.Answers = answers;
                                questions.Add(question);
                            }
                            using (var uow = new UnitOfWork())
                            {
                                uow.QuestionRepository.AddRange(questions);
                                uow.Commit();
                            }
                        }
                    }
                }
            }
            return RedirectToAction("TestList", "TestAdministration");
        }
    }
}