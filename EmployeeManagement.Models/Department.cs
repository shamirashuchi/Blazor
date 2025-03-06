using System.Text.Json.Serialization;

namespace EmployeeManagement.Models
{
    public class Department
    {
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }

        [JsonIgnore]
        public ICollection<Employee> Employees { get; set; }
    }
}
