using EmployeeManagement.Models;

namespace EmployeeManagement.web.Services
{
    public interface IDepartmentService
    
        {
            Task<IEnumerable<Department>> GetDepartments();
            Task<Department> GetDepartment(int id);
        }
    
}
