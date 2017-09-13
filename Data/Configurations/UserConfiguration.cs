using System.Data.Entity.ModelConfiguration;
using Models;

namespace Data.Configurations
{
    public class UserConfiguration : EntityTypeConfiguration<User>
    {
        public UserConfiguration()
        {
            ToTable("User");
            Property(g => g.Username).IsRequired().HasMaxLength(25);
            Property(g => g.UserId).IsRequired();
            Property(g => g.Password).IsRequired().HasMaxLength(25);
            //Property(g => g.Role).IsOptional();
        }
    }
}
