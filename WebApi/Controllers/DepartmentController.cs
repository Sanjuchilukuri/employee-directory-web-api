using Application.DTO;
using Application.Interfaces;
using System.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private IDepartmentsAndRolesServices _departmentsAndRolesServices;

        public DepartmentController(IDepartmentsAndRolesServices departmentsAndRolesServices)
        {
            _departmentsAndRolesServices = departmentsAndRolesServices;
        }

        [HttpGet]
        [Authorize(Roles = "User,Admin")]
        public async Task<ActionResult<List<DepartmentDTO>>> GetDepartments()
        {
            List<DepartmentDTO> allDepartments = await _departmentsAndRolesServices.GetDepartmentsAsync();
            if (allDepartments.Count > 0)
            {
                return Ok(allDepartments);
            }
            else
            {
                return NotFound("No departments exists");
                // throw new Exception("No departments exists");
            }
            throw new Exception("some unknow error occured");
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<AddDepartmentDTO>> AddDepartment([FromBody] AddDepartmentDTO addDepartment)
        {
            if (addDepartment == null || addDepartment.DepartmentName.Trim() == string.Empty)
            {
                throw new ArgumentNullException("properties can't be Empty");
            }

            if (await _departmentsAndRolesServices.AddDepartmentAsync(addDepartment))
            {
                return Ok(addDepartment);
            }
            throw new DbUpdateException("A conflict occurred while updating the database. This may be due to duplicate data");
        }
    }
}