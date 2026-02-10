using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using StudentEnrollmentApi.Data;
using StudentEnrollmentApi.Models;  
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);


// 1. Get the connection string from appsettings.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// 2. Register the DbContext
builder.Services.AddDbContext<ApplicationDBContext>(options =>
    options.UseSqlServer(connectionString));

    
// now adding the identity system to the application
builder.Services.AddIdentity< User , IdentityRole>()
.AddEntityFrameworkStores<ApplicationDBContext>()
.AddDefaultTokenProviders();


var jwtKey = builder.Configuration["JWT:Key"];

// This is a safety check for jwt key.
if (string.IsNullOrEmpty(jwtKey))
{
    throw new Exception("JWT Key is missing! Check User Secrets or Environment Variables.");
}

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,

        ValidIssuer = builder.Configuration["JWT:Issuer"],
        ValidAudience = builder.Configuration["JWT:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
    };
});


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


app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.Run();