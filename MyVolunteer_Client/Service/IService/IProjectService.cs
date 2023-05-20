using MyVolunteer_Models;

namespace MyVolunteer_Client.Service.IService
{
    public interface IProjectService
    {
        public Task<IEnumerable<ProjectDTO>> GetAll();
        public Task<ProjectDTO> Get(int projectId);
    }
}
