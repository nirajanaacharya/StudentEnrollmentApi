using Microsoft.AspNetCore.Identity;
using StudentEnrollmentApi.DTOs; // Add this

namespace StudentEnrollmentApi.Services.Interfaces
{
    public interface IAuthService
    {
        Task<IdentityResult> RegisterAsync(RegisterDto model);
        Task<string?> LoginAsync(LoginDto model);
    }
}