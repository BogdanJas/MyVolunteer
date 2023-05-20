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

        [HttpGet("{projectSignUpHeaderId}")]
        public async Task<IActionResult> Get(int? projectSignUpHeaderId)
        {
            if(projectSignUpHeaderId == null || projectSignUpHeaderId == 0)
            {
                return BadRequest(new ErrorModelDTO()
                {
                    ErrorMessage="Invalid Id",
                    StatusCode=StatusCodes.Status400BadRequest
                });
            }
            var projectSignUpHeader = await _projectSignUpRepository.Get(projectSignUpHeaderId.Value);

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

    }
}
