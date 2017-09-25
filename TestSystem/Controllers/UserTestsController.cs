using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Data;
using TestSystem.Models.UserTests;
using TestSystem.ViewModels.UserTests;

namespace TestSystem.Controllers
{
    [Authorize]
    public class UserTestsController : Controller
    {
        /// <summary>
        /// Get, logged-in user tests
        /// </summary>
        /// <returns>All active user tests list</returns>
        public ActionResult TestList()
        {
            var name = HttpContext.User.Identity.Name;
            using (var uow = new UnitOfWork())
            {
                var userTests = uow.UserTestRepository.GetActivateUserTestsByUsername(name);
                var testListViewModel = Mapper.Map<List<TestListViewModel>>(userTests);
                return View(testListViewModel);
            }
        }

        /// <summary>
        /// Set test satart time and start the timer
        /// </summary>
        /// <param name="userTestId">Which test to load</param>
        /// <returns>Information about test to view</returns>
        public ActionResult Test(int userTestId)
        {
            using (var uow = new UnitOfWork())
            {
                var userTest = uow.UserTestRepository.GetUserTestById(userTestId);
                // Set test start time, when did user start test
                if (userTest.TestStart == null)
                {
                    userTest.TestStart = DateTime.Now;
                    uow.Commit();
                }
                
                var duration = userTest.Test.Duration;
                var testViewModel = new TestViewModel
                {
                    TestId = userTestId,
                    TestName = uow.TestRepository.GetTestById(userTest.TestId).Name,
                    TimeLeft = (long)(userTest.TestStart.Value.AddMinutes(duration) - DateTime.Now).TotalSeconds,
                    TotalQuestions = userTest.UserAnswers.Count,
                    QuestionsLeft = userTest.UserAnswers.Select(s => s.AnswerId != null).Count()
                };
                return View(testViewModel);
            }
        }

        /// <summary>
        /// Get next test question and answers
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Next user question with answer / no more questions go to TestFinish action</returns>
        public ActionResult TestPartial(int id)
        {
            using (var uow = new UnitOfWork())
            {
                var userAnswer = uow.UserAnswerRepository.GetNextUserAnswer(id);
                if (userAnswer == null )
                {
                    return RedirectToAction("TestFinish",new {id});
                }
                var testPartialViewModel = Mapper.Map<TestPartialViewModel>(userAnswer);
                return PartialView(testPartialViewModel);
            }
        }

        /// <summary>
        /// Post user answer to DB and Get next question
        /// </summary>
        /// <param name="userAnswerModel">Data with </param>
        /// <param name="answer">User selected answer</param>
        /// <returns>Next question go to TestPartial action / 
        /// model state wrong or no more questions go to user test list page</returns>
        [HttpPost]
        public ActionResult TestPartial(UserAnswerModel userAnswerModel,string answer)
        {
            if (!ModelState.IsValid || answer == null) return RedirectToAction("TestList");
            using (var uow = new UnitOfWork())
            {
                var userAnswer = uow.UserAnswerRepository.Get(userAnswerModel.UserAnswerId);
                userAnswer.AnswerId = Convert.ToInt32(answer);
                uow.Commit();
            }
            return RedirectToAction("TestPartial", new {id = userAnswerModel.UserTestId});
        }

        /// <summary>
        /// When user finish test count how long it took and set it as finished
        /// </summary>
        /// <param name="id">user test id</param>
        /// <returns></returns>
        public ActionResult TestFinish(int id)
        {
            using (var uow = new UnitOfWork())
            {
                var userTest = uow.UserTestRepository.GetUserTestById(id);
                var duration = userTest.Test.Duration;
                if (userTest.TestStart != null && userTest.Status != "finished")
                {
                    var timeLeft =  DateTime.Now - userTest.TestStart.Value;
                    if (timeLeft > TimeSpan.FromMinutes(userTest.Test.Duration))
                    {
                        userTest.Time = TimeSpan.FromMinutes(userTest.Test.Duration);
                    }
                    else
                    {
                        userTest.Time = timeLeft;
                    }
                    userTest.Status = "finished";
                    uow.Commit();
                    return new EmptyResult();
                }
                return RedirectToAction("TestList");
            }
        }
    }
}