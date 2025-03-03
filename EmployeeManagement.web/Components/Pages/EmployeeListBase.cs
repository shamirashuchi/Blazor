
using Microsoft.AspNetCore.Components;
using EmployeeManagement.Models;
using EmployeeManagement.web.Services;
namespace EmployeeManagement.web.Components.Pages
{
    public class EmployeeListBase : ComponentBase
    {
        public bool IsLoading { get; set; } = true;
       
        [Inject]
        public IEmployeeService? EmployeeService { get; set; }

        public bool ShowFooter { get; set; } = true;

        public required IEnumerable<Employee> Employees { get; set; }
        public int SelectedEmployeeCount { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Employees = (await EmployeeService.GetEmployees()).ToList();
        }

        protected void EmployeeSelectionChanged(bool isSelected)
        {
            if(isSelected)
            {
                SelectedEmployeeCount++;
            }
            else
            {
                SelectedEmployeeCount--;
            }
        }
    }
}
