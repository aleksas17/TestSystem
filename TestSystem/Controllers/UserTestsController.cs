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
        public ActionResult TestList()
        {
            var name = HttpContext.User.Identity.Name;
            using (var uow = new UnitOfWork())
            {
                var userTests = uow.UserTestRepository.GetActivateUserTestsByUsername(name);
                var userTestModel = Mapper.Map<List<UserTestModel>>(userTests);
                return View(userTestModel);
            }
        }

        public ActionResult Test(int userTestId)
        {
            using (var uow = new UnitOfWork())
            {
                var userTest = uow.UserTestRepository.GetUserTestById(userTestId);
                if (userTest.TestStart == null)
                {
                    userTest.TestStart = DateTime.Now;
                    uow.Commit();
                }
                
                var duration = userTest.Test.Duration;
                var testViewModel = new TestViewModel
                {
                    TestId = userTestId,
                    TimeLeft = (long)(userTest.TestStart.Value.AddMinutes(duration) - DateTime.Now).TotalSeconds,
                    TotalQuestions = userTest.UserAnswers.Count,
                    QuestionsLeft = userTest.UserAnswers.Select(s => s.AnswerId != null).Count()
            };
                return View(testViewModel);
            }
        }

        public ActionResult TestPartial(int id)
        {
            using (var uow = new UnitOfWork())
            {
                //var userTest = uow.UserTestRepository.GetUserTestById(userTestId);
             
                var userAnswer = uow.UserAnswerRepository.GetNextUserAnswer(id);
                //userAnswer.Question.Answers = userAnswer.Question.Answers.OrderBy(r => Guid.NewGuid()).Take(5).ToList();
                if (userAnswer == null )
                {
                    return RedirectToAction("TestFinish",new {id});
                }
                //if (userAnswer == null || userTest.TestStart.Value.AddMinutes(duration)<=DateTime.Now)
                //{
                //    userTest.Status = "finished";
                //    userTest.Time = (Convert.ToDateTime(userTest.TestStart) - DateTime.Now).TotalMinutes;
                //    uow.Commit();
                //    return RedirectToAction("TestList");
                //}
                var testPartialViewModel = Mapper.Map<TestPartialViewModel>(userAnswer);
                return View(testPartialViewModel);
            }
        }

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

        public ActionResult TestFinish(int id)
        {
            using (var uow = new UnitOfWork())
            {
                var userTest = uow.UserTestRepository.GetUserTestById(id);
                var duration = userTest.Test.Duration;
                if (userTest.TestStart != null && userTest.Status !="Finished")
                {
                    var timeLeft = userTest.TestStart.Value.AddMinutes(duration) - DateTime.Now;
                    if (timeLeft < new TimeSpan(0, 0, 0))
                    {
                        userTest.Time = userTest.Test.Duration;
                    }
                    else
                    {
                        
                    }
                    userTest.Status = "finished";
                    userTest.Time = (Convert.ToDateTime(userTest.TestStart) - DateTime.Now).TotalMinutes;
                    uow.Commit();
                    return Content("<div class=\"height-helper\"></div>");
                }
                return RedirectToAction("TestList");
            }
        }
    }
}