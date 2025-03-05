using EmployeeManagement.Models;

namespace EmployeeManagement.web.Services
{
    public interface IEmployeeService
    {
        Task<IEnumerable<Employee>> GetEmployees();
        Task<(Employee employee, List<Department> departments)> GetEmployee(int employeeId);
    }
}
