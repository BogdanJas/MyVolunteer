using Microsoft.AspNetCore.Mvc;
using MyVolunteer_Business.Repository.IRepository;
using MyVolunteer_Models;

namespace MyVolunteer_API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class VolunteerController : ControllerBase
    {
        private readonly IVolunteerRepository _volunteerRepository;

        public VolunteerController(IVolunteerRepository _volunteerRepository)
        {
            this._volunteerRepository = _volunteerRepository;
        }

        [HttpPost]
        public async Task<VolunteerDTO> Add([FromBody] VolunteerDTO volunteer)
        {
            return await _volunteerRepository.Create(volunteer);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] VolunteerDTO volunteer)
        {
            if(volunteer == null)
            {
                return BadRequest();
            }

            var result = await _volunteerRepository.Update(volunteer);

            if (result == null)
            {
                return BadRequest();
            }
            else
                return Ok(result);

        }

        [HttpGet("{volunteerId}")]
        public async Task<IActionResult> Get(int? volunteerId)
        {
            if (volunteerId == null || volunteerId == 0)
            {
                return BadRequest(new ErrorModelDTO()
                {
                    ErrorMessage = "Invalid Id",
                    StatusCode = StatusCodes.Status400BadRequest
                });
            }
            var volunteer = await _volunteerRepository.Get(volunteerId.Value);

            if (volunteer == null)
            {
                return BadRequest(new ErrorModelDTO()
                {
                    ErrorMessage = "Invalid Id",
                    StatusCode = StatusCodes.Status404NotFound
                });
            }
            return Ok(volunteer);
        }


    }
}
