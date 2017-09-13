using System.Collections.Generic;
using Models;

namespace Data.IRepositories
{
    public interface ITestRepository: IBaseRepository<Test>
    {
        Test GetTestById(int id);
        IEnumerable<Test> GetAllByStatus(string status);
        Test GetTestByName(string name);
        IEnumerable<Test> GetAllActivatedTests();
        IEnumerable<Test> SearchByKeyword(string keyword);
    }
}
