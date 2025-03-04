using EmployeeManagement.Models;
using EmployeeManagement.web.Services;
using Microsoft.AspNetCore.Components;

namespace EmployeeManagement.web.Components.Pages
{
    public class EditEmployeeBase : ComponentBase
    {
        public Employee Employee { get; set; } = new Employee();

        [Inject]
        public IEmployeeService EmployeeService { get; set; }

        [Parameter]
        public string Id { get; set; }


        protected async override Task OnInitializedAsync()
        {
            Id = Id ?? "1";
            Employee = await EmployeeService.GetEmployee(int.Parse(Id));
        }
    }
}
