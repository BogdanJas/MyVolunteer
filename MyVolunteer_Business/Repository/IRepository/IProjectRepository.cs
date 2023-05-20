using MyVolunteer_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyVolunteer_Business.Repository.IRepository
{
    public interface IProjectRepository
    {
        public Task<ProjectDTO> Create(ProjectDTO objDTO);
        public Task<ProjectDTO> Update(ProjectDTO objDTO);
        public Task<int> Delete(int id);
        public Task<ProjectDTO> Get(int id);
        public Task<IEnumerable<ProjectDTO>> GetAll();
    }
}
