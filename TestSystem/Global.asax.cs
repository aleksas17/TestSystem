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
                Database.SetInitializer(new DropCreateDatabaseIfModelChanges<TestSystemContext>());
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
                // Map UserTest DB data to UserScoresViewModel, using this for statistics
                cnf.CreateMap<UserTest, UsersScoresViewModel>()
                    .ForMember(a => a.FirstName, b => b.MapFrom(src => src.User.FirstName))
                    .ForMember(a => a.LastName, b => b.MapFrom(src => src.User.Lastname))
                    .ForMember("Score", opt => opt.MapFrom(b => b.UserAnswers.Count(a => a.Answer.IsCorrect == 1)));
                // Map UserTest DB data to UserTestQuestionStatisticsModel, using this for statistics
                //cnf.CreateMap<UserTest, UserTestQuestionStatisticsModel>()
                //    .ForMember(a => a.UserTestAnswers, b => b.MapFrom(src => src.User))
                //    .ForMember(a => a.QuestionModel, b => b.MapFrom(src => src.Test.Questions));
                cnf.CreateMap<User, UserTestAnswersModel>();

                    //.ForMember(a => a.TotalAnswersGood, b => b.MapFrom((src => src.UserAnswers.GroupBy(a => a.QuestionId)).Count(a => a.Answer.IsCorrect == 1)));
                // Map TestCreateViewModel data to Test DB
                cnf.CreateMap<TestCreateViewModel, Test>();
                cnf.CreateMap<QuestionModel, Question>();

                cnf.CreateMap<AnswerModel, Answer>();
                cnf.CreateMap<Answer, AnswerModel>();
                cnf.CreateMap<User, UserModel>();
                cnf.CreateMap<Test, TestModel>();
                cnf.CreateMap<UserModel, User>()
                    .ForAllMembers(a=> a.Condition((src, dest, srcVal, destVal, c) => srcVal != null));
                //cnf.CreateMap<Question, Models.TestAdministration.QuestionModel>().ForMember(a => a.Answers, b => b.MapFrom(src => src.Answers));
                cnf.CreateMap<UserTest, TestListViewModel>();
                cnf.CreateMap<UserAnswer, UserAnswerModel>();
                cnf.CreateMap<Question, QuestionModel>()
                .ForMember(a=>a.TotalAnswersGood, b=>b.MapFrom(src=>src.Answers.Count))
                .ForMember(a=>a.TotalAnswersBad, b=>b.MapFrom(src=>src.Test));
                cnf.CreateMap<Question, UserAnswer>();
                cnf.CreateMap<UserTest, UserStatisticsModel>()
                    .ForMember(a => a.Group,b => b.MapFrom(src => src.User));
                cnf.CreateMap<UserCsvModel, User>();
                //cnf.CreateMap<UserAnswer, TestPartialViewModel>();
                cnf.CreateMap<AssignTestPartialViewModel, UserTest>(); 
            });
            ModelBinders.Binders.Add(typeof(UserCsvModel[]), new CsvModelBinder<UserCsvModel>());
        }
    }
}
