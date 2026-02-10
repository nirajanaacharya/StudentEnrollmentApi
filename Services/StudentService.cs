using Microsoft.EntityFrameworkCore;
using StudentEnrollmentApi.Data;
using StudentEnrollmentApi.Models;
using  StudentEnrollmentApi.Services.Interfaces;

namespace StudentEnrollmentApi.Services
{
    public class StudentService : IStudentService
    {
        private readonly ApplicationDBContext _context;

        public StudentService(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Student>> GetAllStudentsAsync() => 
            await _context.Students.ToListAsync();

        public async Task<Student?> GetStudentByIdAsync(int id) => 
            await _context.Students.FindAsync(id);

        public async Task<Student> CreateStudentAsync(Student student)
        {
            _context.Students.Add(student);
            await _context.SaveChangesAsync();
            return student;
        }

        public async Task<bool> UpdateStudentAsync(int id, Student student)
        {
            if (id != student.Id) return false;
            _context.Entry(student).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteStudentAsync(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null) return false;

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}