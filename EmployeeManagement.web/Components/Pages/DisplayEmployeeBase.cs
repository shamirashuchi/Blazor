using Microsoft.AspNetCore.Components;
using EmployeeManagement.Models;

namespace EmployeeManagement.web.Components.Pages
{
    public class DisplayEmployeeBase : ComponentBase
    {
        [Parameter]
        public required Employee Employee { get; set; }

        [Parameter]
        public bool ShowFooter { get; set; }

        [Parameter]
        public EventCallback<bool> OnEmployeeSelection { get; set; }

        protected async Task CheckBoxChanged(ChangeEventArgs e)
        {
            if (e.Value is bool value)
            {
                // If the value is a boolean, invoke the callback
                await OnEmployeeSelection.InvokeAsync(value);
            }
            else
            {
                // If the value is not a boolean, log an error
                Console.WriteLine("Invalid value type for checkbox change event.");
            }
        }
    }
}
