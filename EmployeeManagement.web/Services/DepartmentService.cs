using EmployeeManagement.Models;

namespace EmployeeManagement.web.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly HttpClient httpClient;

        public DepartmentService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<Department?> GetDepartment(int id)
        {
            try
            {
                return await httpClient.GetFromJsonAsync<Department>($"api/employees/{id}") ?? throw new Exception("Department not found.");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error fetching department (ID: {id}): {ex.Message}");
                return null;
            }
        }


        public async Task<IEnumerable<Department>> GetDepartmentsAsync()
        {
            try
            {
                var departments = await httpClient.GetFromJsonAsync<Department[]>("api/employees");
                return departments ?? Enumerable.Empty<Department>();
            }
            catch (Exception ex)
            {
                // Log or handle the error as necessary
                Console.Error.WriteLine($"Error fetching employees: {ex.Message}");
                return Enumerable.Empty<Department>(); // Return an empty collection in case of error
            }
        }

    }
}

