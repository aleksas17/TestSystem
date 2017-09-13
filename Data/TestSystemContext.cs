using System.Data.Entity;
using Data.Configurations;
using Models;

namespace Data
{
    public class TestSystemContext : DbContext
    {
        public TestSystemContext() : base("TestSystem") { }

        public DbSet<User> Users { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Test> Tests { get; set; }
        public DbSet<UserTest> UserTests { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<UserAnswer> UserAnswers { get; set; }

        public virtual void Commit()
        {
            SaveChanges();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new UserConfiguration());
            modelBuilder.Configurations.Add(new TestConfiguration());
            modelBuilder.Configurations.Add(new QuestionConfiguration());
            modelBuilder.Configurations.Add(new UserTestConfiguration());
            modelBuilder.Configurations.Add(new UserAnswerConfiguration());
            modelBuilder.Configurations.Add(new AnswerConfigurations());
        }
    }
}
