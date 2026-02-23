using System.ComponentModel.DataAnnotations;

namespace StudentEnrollmentApi.Models
{
    public class StudentViewModel
    {
        public List<Student> Students { get; set; } = new List<Student>();
        
       
        public string Name { get; set; } = string.Empty;
        
      
        public string Email { get; set; } = string.Empty;
        
      
        public string Department { get; set; } = string.Empty;
    }
}