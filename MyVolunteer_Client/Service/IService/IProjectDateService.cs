using MyVolunteer_Models;

namespace MyVolunteer_Client.Service.IService
{
    public interface IProjectDateService
    {
        public Task<IEnumerable<ProjectDateDTO>> GetAll();
        public Task<ProjectDateDTO> Get(int projectDateId);
    }
}
