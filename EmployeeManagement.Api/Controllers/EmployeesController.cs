using EmployeeManagement.Api.Models;
using EmployeeManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EmployeeManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeRepository employeeRepository;
        public EmployeesController(IEmployeeRepository employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }

        //[HttpGet("{search}/{name}/{gender}")]
        //public async Task<ActionResult<Enumerable<Employee>>> Search(string name, Gender? gender)
        //{
        //    try 
	       // {
        //        var result = await employeeRepository.Search(name, gender);
        //        if(result.Any())
        //        {
        //            return Ok(result);
        //        }
        //        return NotFound();
	       // }
	       // catch (Exception)
	       // {

        //        return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieviing data from database");
        //    }
        //  }
        
        [HttpGet]
    
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
        {
            try
            {
                var employees = await employeeRepository.GetEmployees();
                if (employees == null || !employees.Any())
                {
                    return NotFound("No employees found.");
                }
                return Ok(employees);
            }
            catch (Exception ex)
            {
                // Log the full exception including inner exceptions (if any)
                Console.WriteLine($"Exception: {ex.Message}");
                Console.WriteLine($"StackTrace: {ex.StackTrace}");

                if (ex.InnerException != null)
                {
                    Console.WriteLine($"InnerException: {ex.InnerException.Message}");
                    Console.WriteLine($"Inner StackTrace: {ex.InnerException.StackTrace}");
                }

                // Return a more detailed error message to the client
                return StatusCode(500, $"Error retrieving data from database: {ex.Message}");
            }
        }



       
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Employee>> GetEmployee(int id)
        {
            try
            {
                var result = await employeeRepository.GetEmployee(id);

                if (result == null) return NotFound();

                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }





        //[HttpPost]
        //public async Task<ActionResult<Employee>> CreateEmployee(Employee employee)
        //{
        //    try
        //    {
        //        if (employee == null)
        //        {
        //            return BadRequest();
        //        }
        //        //var emp = employeeRepository.GetEmployeeByEmail(employee.Email);

        //        //if (emp != null)
        //        //{
        //        //    ModelState.AddModelError("Email", "Employee Email already in use");
        //        //    return BadRequest(ModelState);
        //        //}
        //        var createdEmployee = await employeeRepository.AddEmployee(employee);

        //        return CreatedAtAction(nameof(GetEmployee), new { id = createdEmployee.EmployeeId }, createdEmployee);
        //    }
        //    catch (Exception)
        //    {

        //        return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieviing data from  the database");
        //    }
        //}

        //[HttpPut("{id:int}")]
        //public async Task<ActionResult<Employee>> UpdateEmployee(int id, Employee employee)
        //{
        //    try
        //    {
        //        if (id != employee.EmployeeId)
        //        {
        //            return BadRequest("Employee ID mismatch");
        //        }

          
        //        var (employeeToUpdate, _) = await employeeRepository.GetEmployee(id);

        //        if (employeeToUpdate == null)
        //        {
        //            return NotFound($"Employee with Id = {id} not found");
        //        }

        //        var updatedEmployee = await employeeRepository.UpdateEmployee(employee);
        //        return Ok(updatedEmployee); 
        //    }
        //    catch (Exception)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, "Error updating data");
        //    }
        //}


        //[HttpDelete("{id:int}")]
        //public async Task<ActionResult<Employee>> DeleteEmployee(int id)
        //{
        //    try
        //    {
        //        var (employee, _) = await employeeRepository.GetEmployee(id);

        //        if (employee == null)
        //        {
        //            return NotFound($"Employee with Id = {id} not found");
        //        }

        //        var deletedEmployee = await employeeRepository.DeleteEmployee(id);
        //        return Ok(deletedEmployee);
        //    }
        //    catch (Exception)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting data");
        //    }
        //}

    }
}
