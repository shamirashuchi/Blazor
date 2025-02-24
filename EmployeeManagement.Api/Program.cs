using Microsoft.EntityFrameworkCore;
using EmployeeManagement.Api.Models;

namespace EmployeeManagement.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowMyOrigins", policy =>
                {
                    // Replace with your front-end URL
                    policy.WithOrigins("https://localhost:7177/")  // Web project URL
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });
            // Configure DbContext with SQL Server
            builder.Services.AddDbContextFactory<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DBConnection") ??
                    throw new InvalidOperationException("Connection string 'DBConnection' not found.")));

            // Register dependencies for repositories
            builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();

            // Add services to the container
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();
            app.UseCors("AllowMyOrigins");
            // Configure the HTTP request pipeline
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            // Ensure routing is used before authorization
            app.UseRouting();

            app.UseAuthorization();

            // Map controllers
            app.MapControllers();

            // Run the application
            app.Run();
        }
    }
}
