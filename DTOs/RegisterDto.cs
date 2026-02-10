
namespace StudentEnrollmentApi.DTOs
{
    public class RegisterDto
    {
        public required string Username { get; set; }
        public string Email { get; set; }
        public required string  Password { get; set; }
    }
}