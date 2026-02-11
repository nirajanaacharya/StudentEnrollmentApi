
using Microsoft.EntityFrameworkCore;
using StudentEnrollmentApi.Data;
using StudentEnrollmentApi.Models;
using  StudentEnrollmentApi.Services.Interfaces;
using StudentEnrollmentApi.Helpers; // Add this for pagination helper
using StudentEnrollmentApi.DTOs; // Add this for query parameters
namespace StudentEnrollmentApi.Services
{
   public class StudentService : IStudentService
{
    private readonly ApplicationDBContext _context;
    private readonly ILogger<StudentService> _logger;

    public StudentService(ApplicationDBContext context, ILogger<StudentService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<Student>> GetAllStudentsAsync(StudentQueryParameters queryParams)
    {
        // Log the search attempt
        _logger.LogInformation("Searching students. Name: {Name}, Dept: {Dept}, Page: {Page}", 
            queryParams.Name ?? "All", queryParams.Department ?? "All", queryParams.PageNumber);

        var query = _context.Students.AsQueryable();

        if (!string.IsNullOrEmpty(queryParams.Name))
            query = query.Where(s => s.Name.Contains(queryParams.Name));

        if (!string.IsNullOrEmpty(queryParams.Department))
            query = query.Where(s => s.Department.Contains(queryParams.Department));

        return await query.ApplyPagination(queryParams.PageNumber, queryParams.PageSize).ToListAsync();
    }
        public async Task<Student?> GetStudentByIdAsync(int id)
        {
            var student = await _context.Students.FindAsync(id);

            if (student == null)
            {
                _logger.LogWarning("Student with ID {StudentId} not found.", id);
                return null;
            }

            return student;
        }

        public async Task<Student> CreateStudentAsync(Student student)
        {
            _context.Students.Add(student);
            await _context.SaveChangesAsync();
            
            // Log the successful creation
            _logger.LogInformation("Created new student: {Name} with ID {Id}", student.Name, student.Id);
            
            return student;
        }

        public async Task<bool> UpdateStudentAsync(int id, Student student)
        {
            var existing = await _context.Students.FindAsync(id);
            if (existing == null)
            {
                _logger.LogWarning("Update failed: Student {Id} does not exist.", id);
                return false;
            }

            existing.Name = student.Name;
            existing.Department = student.Department;

            await _context.SaveChangesAsync();
            _logger.LogInformation("Updated student ID {Id} successfully.", id);
            
            return true;
        }

        public async Task<bool> DeleteStudentAsync(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                _logger.LogWarning("Delete failed: Student {Id} not found.", id);
                return false;
            }

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
            
            _logger.LogInformation("Student ID {Id} was deleted.", id);
            
            return true;
        }
    }
}
