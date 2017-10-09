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

        /// <summary>
        /// Check if login valid (username = true and password = true)
        /// </summary>
        public bool CheckIfUserValid(string username, string password)
        {
            return DbSet.Any(a => a.Username == username && a.Password == password);
        }

        /// <summary>
        /// Get user role from DB (Admin / User)
        /// </summary>
        /// <param name="username">Username to check for role</param>
        public string GetUserRole(string username)
        {
            var user = DbSet.SingleOrDefault(a => a.Username == username);
            return user?.Role;
        }

        /// <summary>
        /// Checking if user exist in DB
        /// </summary>
        /// <param name="username">Username we are checking</param>
        public bool CheckIfUsernameExists(string username)
        { 
            return DbSet.Any(a => a.Username == username);
        }

        /// <summary>
        /// Search box for searching users in user list
        /// </summary>
        /// <param name="keyword">The keyword we search</param>
        public IEnumerable<User> SearchByKeyword(string keyword)
        {
            return DbSet.Where(a => (a.Username+" "+a.FirstName+ " " +a.Lastname+ " " +a.Group+ " "+a.Position).Contains(keyword));
        }

        /// <summary>
        /// Adding users from csv file to DB
        /// </summary>
        /// <param name="users">User list</param>
        public void AddUsers(IEnumerable<User> users)
        {
            var usersList = users as IList<User> ?? users.ToList();
            var newUser = usersList.Select(a => a.Username).Distinct().ToArray();
            var usersInDb = DbSet.Where(u => newUser.Contains(u.Username))
                .Select(u => u.Username).ToArray();
            var usersNotInDb = usersList.Where(u => !usersInDb.Contains(u.Username));
            DbSet.AddRange(usersNotInDb);
        }
    }
}
