using System;
using System.Data.Entity;
using System.Linq;
using Data.IRepositories;
using Models;
using System.Collections;
using System.Collections.Generic;

namespace Data.Repositories
{
    public class UserAnswerRepository : BaseRepository<UserAnswer>, IUserAnswerRepository
    {
        public UserAnswerRepository(TestSystemContext testSystemContext) : base(testSystemContext) { }

        public UserAnswer GetNextUserAnswer(int userTestId)
        {
            //.OrderBy(r => Guid.NewGuid()).Take(5)

            var userAnswer = DbSet.Include(a => a.Question.Answers).FirstOrDefault(a => a.AnswerId == null && a.UserTestId == userTestId );
            if (userAnswer != null) userAnswer.Question.Answers = userAnswer.Question.Answers.OrderBy(r => Guid.NewGuid()).Take(5).ToList();
            return userAnswer;
        }

        public IEnumerable<UserAnswer> GetUserAnswersByTestId(int testId)
        {
            var userAnswers = DbSet.Include(a => a.UserTest).Include(a=>a.Answer).Where(a => a.UserTest.TestId == testId && a.UserTest.Status == "finished");
            return userAnswers;
        }
    }
}