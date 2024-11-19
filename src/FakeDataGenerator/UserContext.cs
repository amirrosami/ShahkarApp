using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeDataGenerator
{
    public class UserContext:DbContext
    {
        public DbSet<Users> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<Users>().ToTable("users");
            modelBuilder.Entity<Users>().HasKey(x => x.User_Id);
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=.;Database=ShahkarDb;MultipleActiveResultSets=true;TrustServerCertificate=True;Integrated Security=true;");
            base.OnConfiguring(optionsBuilder);
        }
    }

    public class Users
    {
        public Guid User_Id { get; set; }
        public string Phone_Number { get; set; }
        public string National_Id { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public DateOnly Birth_Date { get; set; }
        public string Address { get; set; }
    }
}
