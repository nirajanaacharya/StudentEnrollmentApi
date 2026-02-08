
// mistake
// namespace StudentEnrollmentApi.Models;



using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore; 

//here i installed the EFCORE packages manually through vscode 

using  StudentEnrollmentApi.Models; 

namespace StudentEnrollmentApi.Data
{
    //DbContext was used to create and interact with the database first, now in order to use the auth system
    // we need to inherit the IdentityDbContext instead of DbContext
    // public class ApplicationDBContext : DbContext


      public class ApplicationDBContext : IdentityDbContext<User>
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options):
         base(options){}


        // this will create the table of name Students in the database
        public DbSet<Student> Students { get; set; }
        
    }
}

