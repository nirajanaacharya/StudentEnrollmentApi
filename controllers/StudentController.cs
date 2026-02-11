using Microsoft.AspNetCore.Mvc;
using StudentEnrollmentApi.Models;
using StudentEnrollmentApi.Services.Interfaces; 
using Microsoft.AspNetCore.Authorization;
using StudentEnrollmentApi.DTOs; 

namespace StudentEnrollmentApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        // Change from Context to Service
        private readonly IStudentService _studentService;
        private readonly ILogger<StudentController> _logger;

        public StudentController(IStudentService studentService, ILogger<StudentController> logger)
        {
            _studentService = studentService;
            _logger = logger;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudents(
           [FromQuery] StudentQueryParameters queryParams)
        {
            var students = await _studentService.GetAllStudentsAsync(queryParams);
            return Ok(students);
        }

        // ✅ GET: api/student/1
        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudent(int id)
        {
            var student = await _studentService.GetStudentByIdAsync(id);

            if (student == null)
                return NotFound();

            return Ok(student);
        }

        // ✅ POST: api/student
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Student>> CreateStudent(Student student)
        {
            var createdStudent = await _studentService.CreateStudentAsync(student);
            return CreatedAtAction(nameof(GetStudent), new { id = createdStudent.Id }, createdStudent);
        }

        // ✅ PUT: api/student/1
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateStudent(int id, Student student)
        {
            var success = await _studentService.UpdateStudentAsync(id, student);
            
            if (!success)
                return BadRequest("Student ID mismatch or update failed");

            return NoContent();
        }

        // ✅ DELETE: api/student/1
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var success = await _studentService.DeleteStudentAsync(id);

            if (!success)
                return NotFound();

            return NoContent();
        }
    }
}