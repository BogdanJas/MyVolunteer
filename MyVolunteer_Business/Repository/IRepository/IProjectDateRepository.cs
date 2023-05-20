using MyVolunteer_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyVolunteer_Business.Repository.IRepository
{
    public interface IProjectDateRepository
    {
        public Task<ProjectDateDTO> Create(ProjectDateDTO objDTO);
        public Task<ProjectDateDTO> Update(ProjectDateDTO objDTO);
        public Task<int> Delete(int id);
        public Task<ProjectDateDTO> Get(int id);
        public Task<IEnumerable<ProjectDateDTO>> GetAll(int? id = null);
    }
}
