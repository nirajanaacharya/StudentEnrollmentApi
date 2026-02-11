using Microsoft.EntityFrameworkCore;
using StudentEnrollmentApi.Data;

namespace StudentEnrollmentApi.DTOs
{
    public class StudentQueryParameters
    {

        public string? Name { get; set; }
        public string? Department { get; set; }

        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}