using Application.Interfaces;
using Application.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Infrastructure.DTO;

namespace WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeServices _employeeServices;

        public EmployeeController(IEmployeeServices employeeServices)
        {
            _employeeServices = employeeServices;
        }


        [HttpGet]
        [Authorize(Roles = "User,Admin")]
        public async Task<ActionResult<EmployeesDTO>> GetAllEmployees()
        {
            List<EmployeesDTO> employees = await _employeeServices.GetEmployeesAsync()!;

            if (employees.Count() > 0)
            {
                return Ok(employees);
            }
            else
            {
                return NotFound("no Employee's Exists");
                // throw new Exception("no Employee's Exists");
            }
            throw new Exception("Some unknow Error Occured ");
        }


        [HttpGet("{id}")]
        [Authorize(Roles = "User,Admin")]
        public async Task<ActionResult<EmployeeDTO>> GetEmployeeById([FromRoute] string id)
        {
            EmployeeDTO employee = await _employeeServices.GetEmployeeAsync(id.Trim());
            if (employee != null)
            {
                return Ok(employee);
            }
            else
            {
                return NotFound($"Employee Not found with {id.Trim()}");
                // throw new Exception($"Employee Not found with {id.Trim()}");
            }
            throw new Exception("Some unknow Error Occured");
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<EmployeeDTO>> DeleteEmployee([FromRoute] string id)
        {
            EmployeeDTO employeeToDelete = await _employeeServices.GetEmployeeAsync(id);

            if (employeeToDelete == null)
            {
                return NotFound($"Employee Not found with {id.Trim()}");
                // throw new Exception($"Employee doesn't exist with {id}");
            }
            else
            {
                if (await _employeeServices.DeleteEmployeeAsync(id))
                {
                    return Ok(employeeToDelete);
                }
            }
            throw new DbUpdateException($"Employee {id} deleted Failed, Some unknow error occured");
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<EmployeeCreateUpdateDTO>> AddEmployee([FromBody] EmployeeCreateUpdateDTO employee)
        {
            if (await _employeeServices.AddEmployeeAsync(employee))
            {
                return Created(nameof(GetEmployeeById), employee);
            }
            throw new DbUpdateException("Failed to Add an Employee");
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<EmployeeCreateUpdateDTO>> UpdateEmployee([FromRoute] string id, [FromBody] EmployeeCreateUpdateDTO updateEmployee)
        {
            if (updateEmployee == null)
            {
                throw new ArgumentException($"Employee not to be null");
            }

            EmployeeDTO employeeToUpdate = await _employeeServices.GetEmployeeAsync(id);

            if (employeeToUpdate == null)
            {
                return NotFound($"Employee Not found with {id.Trim()}");
                // throw new Exception($"Employee Not Found with Id : {id}");
            }

            if (await _employeeServices.UpdateEmployeeAsync(id, updateEmployee))
            {
                return Ok(updateEmployee);
            }

            throw new DbUpdateException("Failed to update");
        }

        [HttpGet("filter")]
        [Authorize(Roles = "User,Admin")]
        public async Task<ActionResult<EmployeesDTO>> GetFilteredEmployees([FromQuery] Filters filter)
        {
            List<EmployeesDTO> filteredEmployees = await _employeeServices.GetFilteredEmployeesAsync(filter);
            if (filteredEmployees.Count > 0)
            {
            return Ok(filteredEmployees);
            }
            else
            {
                return NotFound("no Employee's Exists");
                // throw new Exception("no Employee's Exists");
            }
            throw new Exception("Some unknow Error Occured ");
        }
    }
}