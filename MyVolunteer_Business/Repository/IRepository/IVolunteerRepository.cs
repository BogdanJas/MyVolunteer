using MyVolunteer_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyVolunteer_Business.Repository.IRepository
{
    public interface IVolunteerRepository
    {
        public Task<VolunteerDTO> Create(VolunteerDTO objDTO);
        public Task<VolunteerDTO> Update(VolunteerDTO objDTO);
        public Task<int> Delete(int id);
        public Task<VolunteerDTO> Get(int id);
        public Task<IEnumerable<VolunteerDTO>> GetAll();
    }
}
