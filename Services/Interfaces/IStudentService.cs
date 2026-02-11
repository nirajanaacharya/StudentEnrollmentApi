using StudentEnrollmentApi.Models;
using StudentEnrollmentApi.DTOs;

namespace StudentEnrollmentApi.Services.Interfaces
{
    public interface IStudentService
    {
        // Task<IEnumerable<Student>> GetAllStudentsAsync();

        Task<IEnumerable<Student>> GetAllStudentsAsync(StudentQueryParameters queryParameters);
        Task<Student?> GetStudentByIdAsync(int id);
        Task<Student> CreateStudentAsync(Student student);
        Task<bool> UpdateStudentAsync(int id, Student student);
        Task<bool> DeleteStudentAsync(int id);
    }
}