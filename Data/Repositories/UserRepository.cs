using System.Collections.Generic;
using System.Linq;
using Data.IRepositories;
using Models;

namespace Data.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private readonly TestSystemContext _testSystemContext;
        public UserRepository(TestSystemContext testSystemContext) : base(testSystemContext)
        {
            _testSystemContext = testSystemContext;
        }

        public bool CheckIfUserValid(string username, string password)
        {
            return DbSet.Any(a => a.Username == username && a.Password == password);
        }

        public string GetUserRole(string username)
        {
            var user = DbSet.SingleOrDefault(a => a.Username == username);
            return user?.Role;
        }

        public bool CheckIfUsernameExists(string username)
        { 
            return DbSet.Any(a => a.Username == username);
        }

        public IEnumerable<User> SearchByKeyword(string keyword)
        {
            return DbSet.Where(a => (a.Username+" "+a.FirstName+ " " +a.Lastname+ " " +a.Group+ " "+a.Position).Contains(keyword));
        }

        public void BulkMergeUsers(IEnumerable<User> users)
        {
            _testSystemContext.BulkMerge(users, operation =>
            {
                operation.BatchSize = 500;
                operation.ColumnPrimaryKeyExpression = user => user.Username;
            });
        }
    }
}
