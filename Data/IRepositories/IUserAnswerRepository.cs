using Models;
using System.Collections;
using System.Collections.Generic;

namespace Data.IRepositories
{
    public interface IUserAnswerRepository : IBaseRepository<UserAnswer>
    {
        UserAnswer GetNextUserAnswer(int userTestId);
        IEnumerable<UserAnswer> GetUserAnswersByTestId(int testId);
    }
}
