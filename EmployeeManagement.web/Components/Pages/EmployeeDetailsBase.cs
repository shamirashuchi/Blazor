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
       



    protected void Mouse_Move(MouseEventArgs e)
        {
            Coordinates = $"X = {e.ClientX} Y = {e.ClientY}";
        }

        protected void Button_Click()
        {
            if(ButtonText == "Hide Footer")
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
