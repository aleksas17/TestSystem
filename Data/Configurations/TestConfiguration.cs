using System.Data.Entity.ModelConfiguration;
using Models;

namespace Data.Configurations
{
    public class TestConfiguration : EntityTypeConfiguration<Test>
    {
        public TestConfiguration()
        {
            ToTable("Test");
            Property(g => g.Name).IsRequired().HasMaxLength(25);
            Property(g => g.Language).IsRequired();
            Property(g => g.Status).IsRequired();
            Property(g => g.TestEnd).IsOptional();
            Property(g => g.Duration).IsOptional();
        }
    }
}
