using MyVolunteer_Models;

namespace MyVolunteer_Business.Repository.IRepository
{
    public interface IProjectSignUpRepository
    {
        public Task<ProjectSignUpDTO> Get(int Id);
        public Task<IEnumerable<ProjectSignUpDTO>> GetAll(string? userId = null);
        public Task<ProjectSignUpDTO> Create(ProjectSignUpDTO body);
        public Task<int> Delete(int Id); 
        public Task<ProjectSignUpHeaderDTO> UpdateHeader(ProjectSignUpHeaderDTO body);

    }
}
