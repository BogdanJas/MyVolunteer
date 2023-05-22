using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyVolunteer_Business.Repository.IRepository;
using MyVolunteer_Models;

namespace MyVolunteer_API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProjectSignUpController : ControllerBase
    {
        private readonly IProjectSignUpRepository _projectSignUpRepository;
        public ProjectSignUpController(IProjectSignUpRepository _projectSignUpRepository)
        {
            this._projectSignUpRepository = _projectSignUpRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _projectSignUpRepository.GetAll());
        }

        [HttpGet("{projectSignUpId}")]
        public async Task<IActionResult> Get(int? projectSignUpId)
        {
            if(projectSignUpId == null || projectSignUpId == 0)
            {
                return BadRequest(new ErrorModelDTO()
                {
                    ErrorMessage="Invalid Id",
                    StatusCode=StatusCodes.Status400BadRequest
                });
            }
            var projectSignUpHeader = await _projectSignUpRepository.Get(projectSignUpId.Value);

            if(projectSignUpHeader == null)
            {
                return BadRequest(new ErrorModelDTO()
                {
                    ErrorMessage = "Invalid Id",
                    StatusCode = StatusCodes.Status404NotFound
                });
            }
            return Ok(projectSignUpHeader);

        }
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] ProjectSignUpDTO projectSignUpDTO)
        {
            return Ok(await _projectSignUpRepository.Create(projectSignUpDTO));
        }

    }
}
