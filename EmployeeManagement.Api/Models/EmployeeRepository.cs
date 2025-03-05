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
        public async Task<Employee> AddEmployee(Employee employee)

        {
            var result = await appDbContext.Employees.AddAsync(employee);
            await appDbContext.SaveChangesAsync();
            return result.Entity;
        }
        public async Task<Employee> DeleteEmployee(int employeeId)

        {
            var result = await appDbContext.Employees
             .FirstOrDefaultAsync(e => e.EmployeeId == employeeId);
            if (result != null)
            {
                appDbContext.Employees.Remove(result);
                await appDbContext.SaveChangesAsync();
                return result;
            }
            return null;
        }


        //public async Task<Employee> GetEmployee(int employeeId)
        //{
        //    var employee = await appDbContext.Employees
        //        .Include(e => e.Department)
        //        .FirstOrDefaultAsync(e => e.EmployeeId == employeeId);

        //    if (employee == null)
        //    {
        //        throw new KeyNotFoundException($"Employee with ID {employeeId} not found.");
        //    }

        //    return employee;
        //}


        public async Task<(Employee employee, List<Department> departments)> GetEmployee(int employeeId)
        {
            var employee = await appDbContext.Employees
                .Include(e => e.Department)
                .FirstOrDefaultAsync(e => e.EmployeeId == employeeId);

            if (employee == null)
            {
                throw new KeyNotFoundException($"Employee with ID {employeeId} not found.");
            }

            var departments = await appDbContext.Departments.ToListAsync();

          
            if (employee.Department != null && string.IsNullOrEmpty(employee.Department.DepartmentName))
            {
                throw new InvalidOperationException($"Department name for employee {employee.FirstName} {employee.LastName} is missing.");
            }

            return (employee, departments);
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
                .FirstOrDefaultAsync(e => e.EmployeeId == employee.EmployeeId);
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
