using Application.DTO;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ManagerController : ControllerBase
    {
        private IManagerServices _managerServices;

        public ManagerController(IManagerServices managerServices)
        {
            _managerServices = managerServices;
        }

        [HttpGet]
        [Authorize(Roles = "User,Admin")]
        public async Task<ActionResult<List<CommonDTO>>> GetManagers()
        {
            List<CommonDTO> allManagers = await _managerServices.GetMangersAsync();
            if (allManagers.Count > 0)
            {
                return Ok(allManagers);
            }
            else
            {
                return NotFound("No Managers exists");
                // throw new Exception("No departments exists");
            }
            throw new Exception("some unknow error occured");
        }

    }
}