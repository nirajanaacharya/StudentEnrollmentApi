using Microsoft.EntityFrameworkCore;
using StudentEnrollmentApi.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDBContext>(options =>
    options.UseSqlServer(connectionString));


// This tells that hami sangha controller chha jasle request handle garchha ra response pathauncha
builder.Services.AddControllers(); 

builder.Services.AddOpenApi();

var app = builder.Build();


// Ensurring the application is running
app.MapGet("/", () => "Student Enrollment API is running!");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();