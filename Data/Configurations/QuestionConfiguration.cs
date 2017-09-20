using System.Data.Entity.ModelConfiguration;
using Models;

namespace Data.Configurations
{
    public class QuestionConfiguration:EntityTypeConfiguration<Question>
    {
        public QuestionConfiguration()
        {
            ToTable("Question");
            Property(g => g.Name).IsRequired().HasMaxLength(500);
            Property(g => g.TestId).IsRequired();
        }
    }
}
