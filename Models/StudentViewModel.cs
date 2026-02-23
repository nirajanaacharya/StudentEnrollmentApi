using System.ComponentModel.DataAnnotations;

namespace StudentEnrollmentApi.Models
{
    public class StudentViewModel
    {
        
        public List<Student> Students { get; set; } = new List<Student>();
        
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; } = string.Empty;
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; } = string.Empty;
    
        [Required(ErrorMessage = "Department is required")]
        public string Department { get; set; } = string.Empty;
    }
}