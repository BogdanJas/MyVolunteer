using MyVolunteer_Models;

namespace MyVolunteer_Client.Service.IService
{
    public interface IVolunteerService
    {
        public Task<VolunteerDTO> Get(int volunteerId);
        public Task<SignUpResponseDTO> Add(VolunteerDTO volunteer);
        public Task<VolunteerDTO> Update(VolunteerDTO volunteer);
    }
}
