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
using TestViewModel = TestSystem.ViewModels.TestAdministration.TestViewModel;

namespace TestSystem
{
    public class MvcApplication : HttpApplication
    {
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
            Mapper.Initialize(cnf =>
            {
                cnf.CreateMap<Test, TestTemplatesPartialViewModel>()
                    .ForMember(a => a.QuestionModels, b => b.MapFrom(src => src.Questions));
                cnf.CreateMap<Test, TestTemplatesViewModel>();
                cnf.CreateMap<User, UserModel>();
                cnf.CreateMap<Test, TestModel>();
                cnf.CreateMap<Answer, TestAnswer>();
                cnf.CreateMap<UserModel, User>().ForAllMembers(a=> a.Condition((src, dest, srcVal, destVal, c) => srcVal != null));
                cnf.CreateMap<Question, Models.TestAdministration.QuestionModel>().ForMember(a => a.Answers, b => b.MapFrom(src => src.Answers));
                cnf.CreateMap<Test, TestViewModel>();
                cnf.CreateMap<UserTest, Models.UserTests.UserTestModel>().ForMember("Name", opt => opt.MapFrom(b => b.Test.Name));
                cnf.CreateMap<UserAnswer, UserAnswerModel>();
                cnf.CreateMap<Question, Models.UserTests.QuestionModel>();
                cnf.CreateMap<Answer, AnswerModel>();
                cnf.CreateMap<Question, UserAnswer>();
                cnf.CreateMap<UserTest, UserStatisticsModel>().ForMember(a => a.Group,b => b.MapFrom(src => src.User));
                cnf.CreateMap<UserCsvModel, User>();
                cnf.CreateMap<UserAnswer, TestPartialViewModel>()
                    .ForMember(a => a.QuestionModel, b => b.MapFrom(src => src.Question));
            });
            ModelBinders.Binders.Add(typeof(UserCsvModel[]), new CsvModelBinder<UserCsvModel>());
        }
    }
}
