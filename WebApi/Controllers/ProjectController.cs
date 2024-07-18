using Application.DTO;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private IProjectServices _projectServices;

        public ProjectController(IProjectServices projectServices)
        {
            _projectServices = projectServices;
        }

        [HttpGet]
        [Authorize(Roles = "User,Admin")]
        public async Task<ActionResult<List<CommonDTO>>> GetProjects()
        {
            List<CommonDTO> allProjects = await _projectServices.GetProjectsAsync();
            if (allProjects.Count > 0)
            {
                return Ok(allProjects);
            }
            else
            {
                return NotFound("No projects exists");
                // throw new Exception("No departments exists");
            }
            throw new Exception("some unknow error occured");
        }

    }
}