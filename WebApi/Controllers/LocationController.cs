using Application.DTO;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private ILocationServices _locationServices;

        public LocationController(ILocationServices locationServices)
        {
            _locationServices = locationServices;
        }

        [HttpGet]
        [Authorize(Roles = "User,Admin")]
        public async Task<ActionResult<List<CommonDTO>>> GetLocations()
        {
            List<CommonDTO> allLocations = await _locationServices.GetLoacationsAsync();
            if (allLocations.Count > 0)
            {
                return Ok(allLocations);
            }
            else
            {
                return NotFound("No locations exists");
                // throw new Exception("No departments exists");
            }
            throw new Exception("some unknow error occured");
        }

    }
}