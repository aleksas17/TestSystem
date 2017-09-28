using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Data.IRepositories;
using Models;

namespace Data.Repositories
{
    class UserTestRepository : BaseRepository<UserTest>, IUserTestRepository
    {
        public UserTestRepository(TestSystemContext testSystemContext) : base(testSystemContext)
        { }

        /// <summary>
        /// Get all test that are "active"
        /// </summary>
        /// <param name="username">Which user test we take</param>
        /// <returns>All active user tests</returns>
        public IEnumerable<UserTest> GetActivateUserTestsByUsername(string username)
        {
            return DbSet.Include(a=>a.Test).Where(a => (a.Status == "Inactive" || a.Status == "Active") && a.User.Username == username);
        }

        /// <summary>
        /// Get user tests by test name
        /// </summary>
        /// <param name="testName">Which test name to look up</param>
        /// <returns>All tests by given name</returns>
        public IEnumerable<UserTest> GetUserTestsByTestName(string testName)
        {
            return DbSet.Include(a=>a.UserAnswers).Include(a=>a.User).Where(a => a.Test.Name == testName);
        }

        /// <summary>
        /// Get user tests by id
        /// </summary>
        /// <param name="id">Which id to look up</param>
        /// <returns>All tests by given id</returns>
        public UserTest GetUserTestById(int id)
        {
            return DbSet.Include(a=>a.Test).SingleOrDefault(x => x.UserTestId == id);
        }

        /// <summary>
        /// Get user tests by test id
        /// </summary>
        /// <param name="testId">Which test id to look up</param>
        /// <returns>All tests by given id</returns>
        public IEnumerable<UserTest> GetUserTestsByTestId(int testId)
        {
            return DbSet.Include(a => a.User).Include(a => a.UserAnswers.Select(b => b.Answer)).Where(a => a.TestId == testId);
        }     
    }
}
