using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Data.IRepositories;
using Models;

namespace Data.Repositories
{
    public class QuestionRepository : BaseRepository<Question>, IQuestionRepository
    {
        public QuestionRepository(TestSystemContext testSystemContext) : base(testSystemContext) { }

        public IEnumerable<Question> GetQuestionsByTestName(string testName)
        {
            return DbSet.Include(a => a.Answers).Where(a => a.Test.Name == testName);
        }

        public IEnumerable<Question> GetQuestionsByTestId(int testId)
        {
            return DbSet.Where(a => a.Test.TestId == testId);
        }
    }
}
