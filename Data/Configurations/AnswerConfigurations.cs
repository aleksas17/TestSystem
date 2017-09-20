using System.Data.Entity.ModelConfiguration;
using Models;

namespace Data.Configurations
{
    public class AnswerConfigurations : EntityTypeConfiguration<Answer>
    {
        public AnswerConfigurations()
        {
            ToTable("Answer");
            Property(p => p.IsCorrect).IsRequired();
            Property(p => p.Name).IsRequired().HasMaxLength(140);
        }
    }
}
