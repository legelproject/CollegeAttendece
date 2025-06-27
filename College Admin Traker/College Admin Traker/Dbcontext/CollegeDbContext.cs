using ClassLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace College_Admin_Traker.Dbcontext
{
    public class CollegeDbContext:DbContext
    {
        public CollegeDbContext(DbContextOptions<CollegeDbContext> options) : base(options)
        {
            
        }
        public DbSet<Users> Users { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<Userdetails> Userdetail { get; set; }
        public DbSet<Attendence> Attendence { get; set; }
    }
}
