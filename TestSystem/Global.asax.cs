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
                // Map UserTest DB data to UserScoresViewModel
                cnf.CreateMap<UserTest, UsersScoresViewModel>()
                    .ForMember(a => a.FirstName, b => b.MapFrom(src => src.User.FirstName))
                    .ForMember(a => a.LastName, b => b.MapFrom(src => src.User.Lastname))
                    .ForMember("Score", opt => opt.MapFrom(b => b.UserAnswers.Count(a => a.Answer.IsCorrect == 1)));
                // Map TestCreateViewModel data to Test DB
                cnf.CreateMap<TestCreateViewModel, Test>();
                //
                cnf.CreateMap<QuestionModel, Question>();
                cnf.CreateMap<AnswerModel, Answer>();
                cnf.CreateMap<Answer, AnswerModel>();

                //cnf.CreateMap<Question, Models.TestAdministration.QuestionModel>()
                //    .ForMember(dest => dest.Answers, opt => opt.MapFrom(src => src.Answers));

                //cnf.CreateMap<Question, Models.TestAdministration.QuestionModel>();
                //cnf.CreateMap<Question, Models.TestAdministration.QuestionModel>()
                //    .ConstructUsing(ct => Mapper.Map<Models.TestAdministration.QuestionModel>(ct.Answers))
                //    .ForAllMembers(opt => opt.Ignore());
                //cnf.CreateMap<Question, Models.TestAdministration.QuestionModel>()
                //    .ForMember(a => a.Answers, b => b.MapFrom(src => src.Answers));

                //cnf.CreateMap<Answer, Models.TestAdministration.AnswerModel>();

                //cnf.CreateMap<Test, TestTemplatesPartialViewModel>()
                //    .ForMember(a => a.QuestionModels, b => b.MapFrom(src => src.Questions));
                //cnf.CreateMap<Test, TestTemplatesViewModel>();

                cnf.CreateMap<User, UserModel>();
                cnf.CreateMap<Test, TestModel>();
                //cnf.CreateMap<Answer, TestAnswer>();
                cnf.CreateMap<UserModel, User>().ForAllMembers(a=> a.Condition((src, dest, srcVal, destVal, c) => srcVal != null));
                //cnf.CreateMap<Question, Models.TestAdministration.QuestionModel>().ForMember(a => a.Answers, b => b.MapFrom(src => src.Answers));
                //cnf.CreateMap<Test, TestViewModel>(); <- if somthin dosen't work uncoment
                cnf.CreateMap<UserTest, TestListViewModel>().ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Test.Name));
                cnf.CreateMap<UserAnswer, UserAnswerModel>();
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
    }
}
