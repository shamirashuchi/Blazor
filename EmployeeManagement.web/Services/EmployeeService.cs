using EmployeeManagement.Models;
using EmployeeManagement.web.Components.Pages;
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



        public async Task<(Employee employee, List<Department> departments)> GetEmployee(int employeeId)
        {
            try
            {
                // Fetch employee data from the API
                var employeeResponse = await httpClient.GetAsync($"api/Employees/employeedetails/{employeeId}");

                // Check if the response was successful
                if (!employeeResponse.IsSuccessStatusCode)
                {
                    throw new Exception($"Error fetching employee (ID: {employeeId}). Status: {employeeResponse.StatusCode}");
                }

                // Deserialize the employee data
                var employee = await employeeResponse.Content.ReadFromJsonAsync<Employee>();

                // If employee is not found, throw an error
                if (employee == null)
                {
                    throw new Exception("Employee not found.");
                }

                // Fetch department data
                var departmentsResponse = await httpClient.GetAsync("api/Departments");

                // Check if the response for departments was successful
                if (!departmentsResponse.IsSuccessStatusCode)
                {
                    throw new Exception("Error fetching departments data.");
                }

                // Deserialize the departments data
                var departments = await departmentsResponse.Content.ReadFromJsonAsync<List<Department>>();

                // If departments list is null or empty, throw an error
                if (departments == null || !departments.Any())
                {
                    throw new Exception("Departments not found.");
                }

                // Return the employee and departments as a tuple
                return (employee, departments);
            }
            catch (Exception ex)
            {
                // Log the error message
                Console.Error.WriteLine($"Error fetching employee (ID: {employeeId}): {ex.Message}");

                // Optionally, you could log the exception to a file or structured logging system
                // You could also rethrow the exception or return a default value to handle failure more gracefully

                // Return null values indicating failure (for now, to keep existing logic)
                return (null, null);
            }
        }


    }
}

    


