using EmployeeManagement.Models;
using EmployeeManagement.web.Services;
using Microsoft.AspNetCore.Components;

namespace EmployeeManagement.web.Components.Pages
{
    public class EditEmployeeBase : ComponentBase
    {
        public Employee Employee { get; set; } = new Employee();
        public List<Department> Departments { get; set; } = new List<Department>();

        [Inject]
        public IEmployeeService EmployeeService { get; set; }


        [Inject]
        public IDepartmentService DepartmentService { get; set; }

        [Parameter]
        public string Id { get; set; }


     



        protected async override Task OnInitializedAsync()
        {
            Id = Id ?? "1";
            try
            {
                var result = await EmployeeService.GetEmployee(int.Parse(Id));

                Employee = result.employee;
                Departments = result.departments;

                if (Employee == null)
                {
                  
                    Console.Error.WriteLine("Employee not found.");
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error fetching data: {ex.Message}");
            }
        }


        private async Task<IEnumerable<Department>> GetDepartmentsAsync()
        {
            return await DepartmentService.GetDepartmentsAsync();
        }


    }
}

