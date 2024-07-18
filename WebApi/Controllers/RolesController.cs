using Application.DTO;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private IDepartmentsAndRolesServices _departmentsAndRolesServices;

        public RolesController(IDepartmentsAndRolesServices departmentsAndRolesServices)
        {
            _departmentsAndRolesServices = departmentsAndRolesServices;
        }


        [HttpGet("{departmentId}")]
        [Authorize(Roles = "User,Admin")]
        public async Task<ActionResult<List<RolesDTO>>> GetDepartmentRoles([FromRoute] int departmentId)
        {
            List<RolesDTO> departmentRoles = await _departmentsAndRolesServices.GetDepartmentRolesAsync(departmentId);
            if (departmentRoles.Count > 0)
            {
                return Ok(departmentRoles);
            }
            else
            {
                return NotFound($"No roles Exist or department doesn't exists");
                // throw new Exception($"No roles Exist in {department.Trim()} or {department.Trim()} doesn't exists");
            }
            throw new Exception("some unknow error occured");
        }

        [HttpGet]
        [Authorize(Roles = "User,Admin")]
        public async Task<ActionResult<List<RolesDTO>>> GetRoles()
        {
            List<AllRolesDTO> allRoles = await _departmentsAndRolesServices.GetAllRolesAsync();
            if (allRoles.Count > 0)
            {
                return Ok(allRoles);
            }
            else
            {
                return NotFound($"No roles Exist or department doesn't exists");
                // throw new Exception($"No roles Exist in {department.Trim()} or {department.Trim()} doesn't exists");
            }
            throw new Exception("some unknow error occured");
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<AddRoleDTO>> AddRole([FromBody] AddRoleDTO addRole)
        {

            if (addRole == null || addRole.Id == 0 || addRole.Name == string.Empty)
            { 
                throw new ArgumentNullException("Parameters never be empty");
            }

            if (await _departmentsAndRolesServices.AddRoleAsync(addRole))
            {
                return Ok("Role added successfully");
            }
            else
            {
                throw new DbUpdateException("Role already Exist's");
            }
            throw new Exception("Failed to add role");
        }
    }
}