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

        /// <summary>
        /// Get test by its ID
        /// </summary>
        /// <param name="id">Test id</param>
        /// <returns>Test</returns>
        public Test GetTestById(int id)
        {
            return DbSet.Include(a => a.Questions.Select(b => b.Answers)).Include(a=>a.UserTests)
                .SingleOrDefault(a => a.TestId == id);
        }

        /// <summary>
        /// Get test by its name
        /// </summary>
        /// <param name="name">Test name</param>
        /// <returns>Test</returns>
        public Test GetTestByName(string name)
        {
            return DbSet.SingleOrDefault(a => a.Name == name);
        }

        /// <summary>
        /// Get all active tests for user
        /// </summary>
        /// <returns>Active test list</returns>
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
