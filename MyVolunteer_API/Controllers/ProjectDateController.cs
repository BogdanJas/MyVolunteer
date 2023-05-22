using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyVolunteer_Business.Repository.IRepository;
using MyVolunteer_Models;

namespace MyVolunteer_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectDateController : ControllerBase
    {
        private readonly IProjectDateRepository _projectDateRepository;
        public ProjectDateController(IProjectDateRepository projectDateRepository)
        {
            _projectDateRepository = projectDateRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _projectDateRepository.GetAll());
        }

        [HttpGet("{projectDateId}")]
        public async Task<IActionResult> Get(int? projectDateId)
        {
            if(projectDateId == null || projectDateId == 0)
            {
                return BadRequest(new ErrorModelDTO()
                {
                    ErrorMessage="Invalid Id",
                    StatusCode=StatusCodes.Status400BadRequest
                });
            }
            var project = await _projectDateRepository.Get(projectDateId.Value);

            if(project == null)
            {
                return BadRequest(new ErrorModelDTO()
                {
                    ErrorMessage = "Invalid Id",
                    StatusCode = StatusCodes.Status404NotFound
                });
            }
            return Ok(project);

        }

    }
}
