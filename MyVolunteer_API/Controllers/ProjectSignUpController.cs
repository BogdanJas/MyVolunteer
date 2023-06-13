using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.UI.Services;
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
        private readonly IEmailSender _emailSender;
        public ProjectSignUpController(IProjectSignUpRepository _projectSignUpRepository, IEmailSender _emailSender)
        {
            this._projectSignUpRepository = _projectSignUpRepository;
            this._emailSender = _emailSender;
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
        public async Task<IActionResult> Add([FromBody] ProjectSignUpDTO projectSignUp)
        {
            try
            {
                await _projectSignUpRepository.Create(projectSignUp);
                await _emailSender.SendEmailAsync(projectSignUp.VolunteerEmail, "Volunteer sign up confirmation", $"Hi, {projectSignUp.VolunteerName} <br>Thank you for sign up. We're welcome to start your project named \"{projectSignUp.ProjectName}\" which starts {projectSignUp.ProjectStartDate?.ToString("d")} and take place in {projectSignUp.ProjectPlace}. <br>Hope to see you soon :)");

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Ok();
        }

    }
}
