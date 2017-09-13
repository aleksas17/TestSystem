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

        public IEnumerable<UserTest> GetActivateUserTestsByUsername(string username)
        {
            return DbSet.Include(a=>a.Test).Where(a => (a.Status == "Inactive" || a.Status == "Active") && a.User.Username == username);
        }

        public IEnumerable<UserTest> GetUserTestsByTestName(string testName)
        {
            return DbSet.Include(a=>a.UserAnswers).Include(a=>a.User).Where(a => a.Test.Name == testName);
        }

        public UserTest GetUserTestById(int id)
        {
            return DbSet.Include(a=>a.Test).SingleOrDefault(x => x.UserTestId == id);
        }
    }
}
