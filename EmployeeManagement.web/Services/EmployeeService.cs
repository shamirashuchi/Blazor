using EmployeeManagement.Models;
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

        public async Task<Employee?> GetEmployee(int id)
        {
            try
            {
                return await httpClient.GetFromJsonAsync<Employee>($"api/employees/{id}") ?? throw new Exception("Employee not found.");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error fetching employee (ID: {id}): {ex.Message}");
                return null;
            }
        }


        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            try
            {
                var employees = await httpClient.GetFromJsonAsync<Employee[]>("api/employees");
                return employees ?? Enumerable.Empty<Employee>(); // Return an empty collection if null
            }
            catch (Exception ex)
            {
                // Log or handle the error as necessary
                Console.Error.WriteLine($"Error fetching employees: {ex.Message}");
                return Enumerable.Empty<Employee>(); // Return an empty collection in case of error
            }
        }

    }
}
