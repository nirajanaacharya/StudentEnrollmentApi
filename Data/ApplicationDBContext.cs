using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore; 

using  StudentEnrollmentApi.Models; 

namespace StudentEnrollmentApi.Data
{
    // public class ApplicationDBContext : DbContext


      public class ApplicationDBContext : IdentityDbContext<User>
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options):
         base(options){}
        public DbSet<Student> Students { get; set; }
        
    }
}

