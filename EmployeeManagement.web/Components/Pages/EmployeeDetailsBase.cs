using EmployeeManagement.Models;
using EmployeeManagement.web.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace EmployeeManagement.web.Components.Pages
{
    public class EmployeeDetailsBase : ComponentBase
    {
        
       
        protected string Coordinates { get; set; }

        protected string ButtonText { get; set; } = "Hide Footer";
        protected string CssClass { get; set; } = null;

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
            try
            {
                // Check if EmployeeId is valid and try to parse it
                if (int.TryParse(Id, out int employeeId)) // Changed 'Id' to 'employeeId'
                {
                    // Fetch the employee and department data
                    var result = await EmployeeService.GetEmployee(employeeId);

                    // Check if employee and departments are returned
                    if (result != null && result.Department != null)
                    {
                        Employee = result;
                        Departments = new List<Department> { result.Department };
                    }
                    else
                    {
                        Console.Error.WriteLine("Employee or Departments are null.");
                    }
                }
                else
                {
                    Console.Error.WriteLine("Invalid EmployeeId format.");
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error fetching data: {ex.Message}");
            }
        }


        // Mouse Move to track coordinates
        protected void Mouse_Move(MouseEventArgs e)
        {
            Coordinates = $"X = {e.ClientX} Y = {e.ClientY}";
        }

        // Toggle footer visibility
        protected void Button_Click()
        {
            if (ButtonText == "Hide Footer")
            {
                ButtonText = "Show Footer";
                CssClass = "HideFooter";
            }
            else
            {
                CssClass = null;
                ButtonText = "Hide Footer";
            }
        }
    }
}
