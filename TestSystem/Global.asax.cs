using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using AutoMapper;
using Data;
using Models;
using TestSystem.Models.Account;
using TestSystem.Models.TestAdministration;
using TestSystem.Models.UserTests;
using TestSystem.Utilities;
using TestSystem.ViewModels.TestAdministration;
using TestSystem.ViewModels.UserTests;
using System;
using TestSystem.Controllers;
//using TestViewModel = TestSystem.ViewModels.TestAdministration.TestViewModel;

namespace TestSystem
{
    public class MvcApplication : HttpApplication
    {
        /// <summary>
        /// When application starts
        /// </summary>
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            using (var db = new TestSystemContext())
            {
                Database.SetInitializer(new CreateDatabaseIfNotExists<TestSystemContext>());
                db.Database.Initialize(true);
            }

            /// <summary>
            /// Automapper librery (automapper.org)
            /// 
            /// AutoMapper is a simple little library built to solve a deceptively complex
            /// problem - getting rid of code that mapped one object to another.
            /// </summary>
            /// Creating maps between one object to another
            Mapper.Initialize(cnf =>
            {
                // Map UserTest DB data to UserScoresViewModel
                cnf.CreateMap<UserTest, UsersScoresViewModel>()
                    .ForMember(a => a.FirstName, b => b.MapFrom(src => src.User.FirstName))
                    .ForMember(a => a.LastName, b => b.MapFrom(src => src.User.Lastname))
                    .ForMember("Score", opt => opt.MapFrom(b => b.UserAnswers.Count(a => a.Answer.IsCorrect == 1)));
                // Map TestCreateViewModel data to Test DB
                cnf.CreateMap<TestCreateViewModel, Test>();
                cnf.CreateMap<QuestionModel, Question>();
                cnf.CreateMap<AnswerModel, Answer>();
                cnf.CreateMap<Answer, AnswerModel>();
                cnf.CreateMap<Test, TestStatisticsQuestionViewModel>();
                // Map UserTest DB data to UserTestQuestionStatisticsModel, using this for statistics
                cnf.CreateMap<UserTest, TestStatisticsQuestionViewModel>()
                    .ForMember(a => a.UserTestAnswer, b => b.MapFrom(src => src.User))
                    .ForMember(a=>a.Answer, b=>b.MapFrom(src =>src.UserAnswers.Select(a=>a.Answer)))
                    .ForMember(a => a.QuestionModel, b => b.MapFrom(src => src.Test.Questions));
                cnf.CreateMap<User, UserTestAnswersModel>();
                cnf.CreateMap<User, UserModel>();
                cnf.CreateMap<Test, TestModel>();
                cnf.CreateMap<UserModel, User>().ForAllMembers(a=> a.Condition((src, dest, srcVal, destVal, c) => srcVal != null));
                cnf.CreateMap<UserTest, TestListViewModel>();
                cnf.CreateMap<UserAnswer, UserAnswerModel>();
                cnf.CreateMap<UserAnswerModel, UserAnswer>();
                cnf.CreateMap<Question, QuestionModel>();
                cnf.CreateMap<QuestionModel, Question>();
                cnf.CreateMap<Question, UserAnswer>();
                cnf.CreateMap<UserTest, UserStatisticsModel>().ForMember(a => a.Group,b => b.MapFrom(src => src.User));
                cnf.CreateMap<UserCsvModel, User>();
                cnf.CreateMap<UserAnswer, TestPartialViewModel>()
                    .ForMember(a => a.QuestionModel, b => b.MapFrom(src => src.Question));
                cnf.CreateMap<AssignTestPartialViewModel, UserTest>(); 
            });
            ModelBinders.Binders.Add(typeof(UserCsvModel[]), new CsvModelBinder<UserCsvModel>());
        }

        /// <summary>
        /// Error redirector to my custom error pages
        /// </summary>
        protected void Application_Error(object sender, EventArgs e)
        {
            Exception ex = Server.GetLastError();
            HttpException httpex = ex as HttpException;
            RouteData data = new RouteData();
            data.Values.Add("controller", "Error");
            if (httpex == null)
            {
                data.Values.Add("action", "gener");
            }
            else
            {
                switch(httpex.GetHttpCode())
                {
                    // Page not found
                    case 404:
                        data.Values.Add("action", "Http404");
                        break;
                    // Method Not Allowed
                    case 405:
                        data.Values.Add("action", "Http405");
                        break;
                    // All other errors
                    default:
                        data.Values.Add("action", "General");
                        break;
                }
            }
            Server.ClearError();
            Response.TrySkipIisCustomErrors = true;
            IController error = new ErrorController();
            error.Execute(new RequestContext(new HttpContextWrapper(Context), data));
        }
    }
}
