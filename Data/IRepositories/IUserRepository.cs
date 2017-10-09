using System.Collections.Generic;
using Models;

namespace Data.IRepositories
{
    public interface IUserRepository : IBaseRepository<User>
    {
        bool CheckIfUserValid(string username, string password);
        string GetUserRole(string username);
        bool CheckIfUsernameExists(string username);
        IEnumerable<User> SearchByKeyword(string keyword);
        void AddUsers(IEnumerable<User> users);
    }
}
