using Microsoft.EntityFrameworkCore;
using Shahkar.UserManagement.Db.Query.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Shahkar.UserManagement.Db.Query.Common
{
    public class UsersQueryContext :DbContext
    {
        public UsersQueryContext(DbContextOptions<UsersQueryContext> options) : base(options)
        {
        }

        public DbSet<Users>  Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }
}
