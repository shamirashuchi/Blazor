using EmployeeManagement.Models;
using Microsoft.EntityFrameworkCore;
namespace EmployeeManagement.Api.Models
{
    public class DepartmentRepository(AppDbContext appDbContext) : IDepartmentRepository
    {
        private readonly AppDbContext appDbContext = appDbContext;

        public IEnumerable<EmployeeManagement.Models.Department> GetDepartments()
        {
            return appDbContext.Departments;
        }

        public Department GetDepartment(int departmentId)
        {
            var department = appDbContext.Departments
                .FirstOrDefault(d => d.DepartmentId == departmentId);

            if (department == null)
            {
                throw new NotImplementedException();
            }

            return department;
        }



    }
}
