using System;
using Data.IRepositories;
using Models;

namespace Data
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository UserRepository { get; }
        ITestRepository TestRepository { get; }
        IQuestionRepository QuestionRepository { get; }
        IBaseRepository<Answer> AnsweRepository { get; }
        IUserTestRepository UserTestRepository { get; }
        IUserAnswerRepository UserAnswerRepository { get; }


        int Commit();
    }
}
