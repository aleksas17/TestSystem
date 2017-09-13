using Data.IRepositories;
using Data.Repositories;
using Models;

namespace Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TestSystemContext _testSystemContext;
        public UnitOfWork()
        {
            _testSystemContext = new TestSystemContext();
            UserRepository = new UserRepository(_testSystemContext);
            QuestionRepository = new QuestionRepository(_testSystemContext);
            TestRepository = new TestRepository(_testSystemContext);
            UserTestRepository = new UserTestRepository(_testSystemContext);
            AnsweRepository = new BaseRepository<Answer>(_testSystemContext);
            UserAnswerRepository = new UserAnswerRepository(_testSystemContext);
        }

        public IUserRepository UserRepository { get; }
        public IQuestionRepository QuestionRepository { get; }
        public ITestRepository TestRepository { get; }
        public IUserTestRepository UserTestRepository { get; }
        public IBaseRepository<Answer> AnsweRepository { get; }
        public IUserAnswerRepository UserAnswerRepository { get; }

        public int Commit()
        {
            return _testSystemContext.SaveChanges();
        }

        public void Dispose()
        {
            _testSystemContext.Dispose();
        }
    }
}
