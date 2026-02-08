using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using StudentEnrollmentApi.Data;
using StudentEnrollmentApi.Models;  
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");



// builder.Services.AddDbContext<ApplicationDBContext>(options =>
//     options.UseSqlServer(connectionString));

// now adding the identity system to the application
builder.Services.AddIdentity< User , IdentityRole>()
.AddEntityFrameworkStores<ApplicationDBContext>()
.AddDefaultTokenProviders();



// This tells that hami sangha controller chha jasle request handle garchha ra response pathauncha
builder.Services.AddControllers(); 

builder.Services.AddOpenApi();

var app = builder.Build();


// Ensurring the application is running
app.MapGet("/", () => "Student Enrollment API is running!");

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();