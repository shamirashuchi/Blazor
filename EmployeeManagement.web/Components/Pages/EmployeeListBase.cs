
using Microsoft.AspNetCore.Components;
using EmployeeManagement.Models;
namespace EmployeeManagement.web.Components.Pages
{
    public class EmployeeListBase : ComponentBase
    {
        public IEnumerable<Employee> Employees { get; set; }
    }
}
