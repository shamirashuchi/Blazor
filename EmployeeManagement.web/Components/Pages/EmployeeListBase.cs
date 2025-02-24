
using Microsoft.AspNetCore.Components;
using EmployeeManagement.Models;
using EmployeeManagement.web.Services;
namespace EmployeeManagement.web.Components.Pages
{
    public class EmployeeListBase : ComponentBase
    {
        [Inject]
        public IEmployeeService EmployeeService { get; set; }

        public IEnumerable<Employee> Employees { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Employees = (await EmployeeService.GetEmployees()).ToList();
        }
    }
}
