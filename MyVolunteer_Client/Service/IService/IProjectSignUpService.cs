using MyVolunteer_Models;

namespace MyVolunteer_Client.Service.IService
{
    public interface IProjectSignUpService
    {
        public Task<IEnumerable<ProjectSignUpDTO>> GetAll(string? userId);
        public Task<ProjectSignUpDTO> Get(int projectSignUpId);
        public Task<ProjectSignUpDTO> Add(ProjectSignUpDTO projectSignUp);
    }
}
