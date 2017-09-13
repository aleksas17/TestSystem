﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Data;
using Models;
using TestSystem.Models.TestAdministration;
using PagedList;
using TestSystem.ViewModels.TestAdministration;
using TestSystem.Models.Account;
using TestSystem.Models.UserTests;

namespace TestSystem.Controllers
{
    public class TestAdministrationController : Controller
    {
        /// <summary>
        /// Deletes test from list
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

        [Authorize(Roles ="Admin")]
        [HttpGet]
        public ActionResult AssignTest(int? id)
        {

            var userModels = new List<UserModel>();
            using (var uow = new UnitOfWork())
            {
                IEnumerable<User> users;
                // Geting user list
                users = uow.UserRepository.GetAll();
                foreach (var user in users)
                {
                    var userModel = Mapper.Map<UserModel>(user);
                    userModels.Add(userModel);
                }
                var assignTestPartialViewModel = new AssignTestPartialViewModel();
                assignTestPartialViewModel.UserModel = userModels;
                // Geting test name.
                var test = uow.TestRepository.Get(id);
                assignTestPartialViewModel.Name = test.Name;
                return PartialView("AssignTestPartial", assignTestPartialViewModel);
            }
            
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult AssignTestPartial(UserTestModel userTestModel, string answer)
        {

            var name = answer;
            return View();

        }


        [Authorize(Roles = "Admin")]
        public ActionResult TestList(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = string.IsNullOrEmpty(sortOrder) ? "username_desc" : "";
            //ViewBag.NameSortParm = sortOrder == "Name" ? "name_desc" : "Name";
            //ViewBag.LastNameSortParm = sortOrder == "LastName" ? "lastName_desc" : "LastName";
            //ViewBag.PositionSortParm = sortOrder == "Position" ? "position_desc" : "Position";
            //ViewBag.GroupSortParm = sortOrder == "Group" ? "group_desc" : "Group";

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
                //case "Name":
                //    userModels = userModels.OrderBy(s => s.FirstName).ToList();
                //    break;
                //case "name_desc":
                //    userModels = userModels.OrderByDescending(s => s.FirstName).ToList();
                //    break;
            }
            var pageSize = 10;
            var pageNumber = (page ?? 1);
            return View(testModels.ToPagedList(pageNumber, pageSize));
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