using System.Collections.Generic;
using Models;

namespace Data.IRepositories
{
    public interface IQuestionRepository : IBaseRepository<Question>
    {
        IEnumerable<Question> GetQuestionsByTestName(string testName);
    }
}
