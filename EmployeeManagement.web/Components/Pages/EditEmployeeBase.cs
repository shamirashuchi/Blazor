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
            Employee = await EmployeeService.GetEmployee(int.Parse(Id));
            Departments = (await GetDepartmentsAsync()).ToList();
        }

        private async Task<IEnumerable<Department>> GetDepartmentsAsync()
        {
            return await DepartmentService.GetDepartmentsAsync();
        }


    }
}

