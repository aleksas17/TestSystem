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
using System.Collections.Generic;

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
                cnf.CreateMap<TestCreateViewModel, Test>();
                cnf.CreateMap<Models.TestAdministration.QuestionModel, Question>();
                cnf.CreateMap<Models.TestAdministration.AnswerModel, Answer>();
                cnf.CreateMap<Answer, Models.TestAdministration.AnswerModel>();
                //cnf.CreateMap<Test, TestCreateViewModel>().ForMember(dest => dest.Questions,
                //    opt => opt.MapFrom
                //    (
                //        src => Mapper.Map<List<Question>, List<Models.TestAdministration.QuestionModel>>(src.Questions)
                //    )
                //);

                //cnf.CreateMap<Question, Models.TestAdministration.QuestionModel>().ForMember(dest => dest.Answers,
                //    opt => opt.MapFrom
                //    (
                //        src => Mapper.Map<List<Answer>, List<Models.TestAdministration.AnswerModel>>(src.Answers)
                //    )
                //);

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
                cnf.CreateMap<Test, TestViewModel>();
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
                    //.ForMember(a => a.UserId, b => b.MapFrom(src => src.UserId))
                    //.ForMember(a => a.TestId, b => b.MapFrom(src => src.TestId))
                    //.ForMember(a => a.Status, b => b.MapFrom(src => src.Status))
                    //.ForMember(a => a.TestStart, b => b.MapFrom(src => src.TestStart))
                    //.ForMember(a => a.Time, b => b.MapFrom(src => src.Time));
                
            });
            ModelBinders.Binders.Add(typeof(UserCsvModel[]), new CsvModelBinder<UserCsvModel>());
        }
    }
}
