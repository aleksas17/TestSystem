using System;
using System.Data.Entity;
using System.Linq;
using Data.IRepositories;
using Models;

namespace Data.Repositories
{
    public class UserAnswerRepository : BaseRepository<UserAnswer>, IUserAnswerRepository
    {
        public UserAnswerRepository(TestSystemContext testSystemContext) : base(testSystemContext) { }

        public UserAnswer GetNextUserAnswer(int userTestId)
        {
            var userAnswer = DbSet.Include(a=>a.Question.Answers).OrderBy(r => Guid.NewGuid()).Take(5).FirstOrDefault(a => a.AnswerId == null && a.UserTestId == userTestId );
            if (userAnswer != null) userAnswer.Question.Answers = userAnswer.Question.Answers.OrderBy(r => Guid.NewGuid()).Take(5).ToList();
            return userAnswer;
        }
    }
}