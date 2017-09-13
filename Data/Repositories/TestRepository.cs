using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Data.IRepositories;
using Models;

namespace Data.Repositories
{
    public class TestRepository : BaseRepository<Test>, ITestRepository
    {
        public TestRepository(TestSystemContext testSystemContext) : base(testSystemContext) { }

        public Test GetTestById(int id)
        {
            return DbSet.Include(a => a.Questions.Select(b => b.Answers)).Include(a=>a.UserTests)
                .SingleOrDefault(a => a.TestId == id);
        }

        public IEnumerable<Test> GetAllByStatus(string status)
        {
            return DbSet.Where(a => a.Status == status);
        }

        public Test GetTestByName(string name)
        {
            return DbSet.SingleOrDefault(a => a.Name == name);
        }

        public IEnumerable<Test> GetAllActivatedTests()
        {
            return DbSet.Where(a => a.Status == "Active" || a.Status == "Finished");
        }

        /// <summary>
        /// Search box filter that sort test list
        /// </summary>
        /// <param name="keyword">Search box text</param>
        /// <returns>Sorted list by keyword</returns>
        public IEnumerable<Test> SearchByKeyword(string keyword)
        {
            return DbSet.Where(a => (a.Name).Contains(keyword));
        }
    }
}
