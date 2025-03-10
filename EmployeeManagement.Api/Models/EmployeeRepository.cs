using EmployeeManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Api.Models
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext appDbContext;

        public EmployeeRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<Employee> GetEmployee(int employeeId)
        {
            var result =  await appDbContext.Employees
                .Include(e => e.Department)
                .FirstOrDefaultAsync(e => e.id == employeeId);
            return result;
        }
        public async Task<Employee> AddEmployee(Employee employee)

        {
            var result = await appDbContext.Employees.AddAsync(employee);
            await appDbContext.SaveChangesAsync();
            return result.Entity;
        }
        public async Task<Employee> DeleteEmployee(int employeeId)

        {
            var result = await appDbContext.Employees
             .FirstOrDefaultAsync(e => e.id == employeeId);
            if (result != null)
            {
                appDbContext.Employees.Remove(result);
                await appDbContext.SaveChangesAsync();
                return result;
            }
            return null;
        }


       


        



        public async Task<Employee> GetEmployeeByEmail(string email)
        {
            var employee = await appDbContext.Employees
                .FirstOrDefaultAsync(e => e.Email.ToLower() == email.ToLower());

            if (employee == null)
            {
                throw new KeyNotFoundException($"Employee with email {email} not found.");
            }

            return employee;
        }



        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            return await appDbContext.Employees.ToListAsync();
        }


        public async Task<Employee> UpdateEmployee(Employee employee)
        {
            var result = await appDbContext.Employees
                .FirstOrDefaultAsync(e => e.id== employee.id);
            if(result != null)
            {
                result.FirstName = employee.FirstName;
                result.LastName = employee.LastName;
                result.Email = employee.Email;
                result.DateOfBirth = employee.DateOfBirth;
                result.Gender = employee.Gender;
                result.DepartmentId = employee.DepartmentId;
                result.PhotoPath = employee.PhotoPath;

                await appDbContext.SaveChangesAsync();
                return result;
            }
            return null;
        }

        //public async Task<IEnumerable<Employee>> IEmployeeRepository.Search(string name, Gender? gender)

        //{
        //    IQueryable<Employee> query = appDbContext.Employees;
        //    if (!string.IsNullOrEmpty(name))
        //    {
        //        query = query.Where(e => e.FirstName.Contains(name)
                    
        //              || e.LastName.Contains(name));
        //    }

        //    if (gender  != null)
        //    {
        //        query = query.Where(e => e.Gender == gender);
                      
        //    }
        //    return await query.ToListAsync();
        //}

        
    }
}
