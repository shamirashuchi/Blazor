using EmployeeManagement.Models;
using System.Net.Http;
using System.Net.Http.Json;

namespace EmployeeManagement.web.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly HttpClient httpClient;

        public EmployeeService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<(Employee employee, List<Department> departments)> GetEmployee(int employeeId)
        {
            try
            {
                // Fetch employee data from API
                var employee = await httpClient.GetFromJsonAsync<Employee>($"api/employees/{employeeId}");

                if (employee == null)
                {
                    throw new Exception("Employee not found.");
                }

                // Fetch departments data from API (assuming there's an endpoint for departments)
                var departments = await httpClient.GetFromJsonAsync<List<Department>>("api/departments");

                if (departments == null)
                {
                    throw new Exception("Departments not found.");
                }

                return (employee, departments);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error fetching employee (ID: {employeeId}): {ex.Message}");
                return (null, null);
            }
        }


        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            try
            {
                var employees = await httpClient.GetFromJsonAsync<Employee[]>("api/employees");
                return employees ?? Enumerable.Empty<Employee>();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error fetching employees: {ex.Message}");
                return Enumerable.Empty<Employee>();
            }
        }

    }
}
