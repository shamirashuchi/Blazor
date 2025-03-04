using EmployeeManagement.Models;

namespace EmployeeManagement.web.Services
{
    public interface IDepartmentService
    {
        Task<IEnumerable<Department>> GetDepartmentsAsync();
        Task<Department> GetDepartment(int id);
    }
}
