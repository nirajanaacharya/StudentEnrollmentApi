using Microsoft.AspNetCore.Mvc;
using StudentEnrollmentApi.Services.Interfaces;
using StudentEnrollmentApi.Models; 
using StudentEnrollmentApi.DTOs;

namespace StudentEnrollmentApi.Controllers
{
    public class HomeController : Controller
    {
        private readonly IStudentService _studentService;
        private readonly ILogger<HomeController> _logger;

        public HomeController(IStudentService studentService, ILogger<HomeController> logger)
        {
            _studentService = studentService;
            _logger = logger;
        }

        // GET: Display all students and the form
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            _logger.LogInformation("GET Index called - Loading students");
            var queryParams = new StudentQueryParameters 
            { 
                PageNumber = 1, 
                PageSize = 1000 
            };
            
            var studentList = await _studentService.GetAllStudentsAsync(queryParams);
            
            _logger.LogInformation("Retrieved {Count} students from database", studentList.Count());
            
            var viewModel = new StudentViewModel
            {
                Students = studentList.ToList(),
                Name = string.Empty,
                Email = string.Empty,
                Department = string.Empty
            };
            
            return View(viewModel); 
        }

        // POST: Add a new student
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(StudentViewModel model)
        {
            _logger.LogInformation("POST Index called - Attempting to create student: {Name}", model.Name);

            if (ModelState.IsValid)
            {
                var student = new Student
                {
                    Name = model.Name,
                    Email = model.Email,
                    Department = model.Department
                };

                await _studentService.CreateStudentAsync(student);
                
                _logger.LogInformation("Student created successfully: {Name} (ID: {Id})", student.Name, student.Id);
                
                
                return RedirectToAction(nameof(Index)); 
            }

            // If validation fails, reload the list and show the form again
            _logger.LogWarning("Model validation failed for student creation");
            
            var studentList = await _studentService.GetAllStudentsAsync(new StudentQueryParameters());
            model.Students = studentList.ToList();
            
            return View(model);
        }

    [HttpPost]
    [Route("Home/Delete/{id}")]
    public async Task<IActionResult> Delete(int id)
        {
        await _studentService.DeleteStudentAsync(id);
        return RedirectToAction(nameof(Index));
        }
    }
    }