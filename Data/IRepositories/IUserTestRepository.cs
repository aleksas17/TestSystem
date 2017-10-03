using System.Collections.Generic;
using Models;

namespace Data.IRepositories
{
    public interface IUserTestRepository : IBaseRepository<UserTest>
    {
        IEnumerable<UserTest> GetActivateUserTestsByUsername(string username);
        IEnumerable<UserTest> GetUserTestsByTestName(string testName);
        UserTest GetUserTestById(int id);
        IEnumerable<UserTest> GetUserTestsByTestId(int testId);
        IEnumerable<UserTest> GetUserAnswersByTestId(int testId);
    }
}
