
// mistake
// namespace StudentEnrollmentApi.Models;

using Microsoft.EntityFrameworkCore;  
//here i installed the EFCORE packages manually through vscode 
using StudentEnrollmentApi.Models;    

namespace StudentEnrollmentApi.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options):
         base(options)
        {
        }
        // this will create the table of name Students in the database
        public DbSet<Student> Students { get; set; }
        
    }
}

