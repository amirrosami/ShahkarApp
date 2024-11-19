using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shahkar.UserManagement.Db.Query.Entities;

namespace Shahkar.UserManagement.Db.Query.Common
{
    public class UsersConfig : IEntityTypeConfiguration<Users>
    {
        public void Configure(EntityTypeBuilder<Users> builder)
        {
            builder.ToTable("users");
            builder.HasKey(x => x.User_Id);
        }
    }
}
