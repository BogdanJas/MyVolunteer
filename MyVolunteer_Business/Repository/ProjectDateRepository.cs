using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using MyVolunteer_Business.Repository.IRepository;
using MyVolunteer_DataAccess;
using MyVolunteer_DataAccess.Data;
using MyVolunteer_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyVolunteer_Business.Repository
{
    public class ProjectDateRepository: IProjectDateRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public ProjectDateRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<ProjectDateDTO> Create(ProjectDateDTO objDTO)
        {
            var obj = _mapper.Map<ProjectDateDTO, ProjectDate>(objDTO);
            var addedObj = _db.ProjectDates.Add(obj);
            await _db.SaveChangesAsync();
            return _mapper.Map<ProjectDate, ProjectDateDTO>(addedObj.Entity);
        }

        public async Task<int> Delete(int id)
        {
            var obj = await _db.ProjectDates.FirstOrDefaultAsync(u => u.Id == id);
            if (obj != null)
            {
                _db.ProjectDates.Remove(obj);
                return await _db.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<ProjectDateDTO> Get(int id)
        {
            var obj = await _db.ProjectDates.FirstOrDefaultAsync(u => u.Id == id);
            if (obj != null)
            {
                return _mapper.Map<ProjectDate, ProjectDateDTO>(obj);
            }
            return new ProjectDateDTO();
        }

        public async Task<IEnumerable<ProjectDateDTO>> GetAll(int? id = null)
        {
            if (id != null && id > 0)
            {
                return _mapper.Map<IEnumerable<ProjectDate>, IEnumerable<ProjectDateDTO>>
                    (_db.ProjectDates.Where(u => u.ProjectId == id));
            }
            else
            {
                return _mapper.Map<IEnumerable<ProjectDate>, IEnumerable<ProjectDateDTO>>(_db.ProjectDates);
            }
        }

        public async Task<ProjectDateDTO> Update(ProjectDateDTO objDTO)
        {
            var objFromDb = await _db.ProjectDates.FirstOrDefaultAsync(u => u.Id == objDTO.Id);
            if (objFromDb != null)
            {
                objFromDb.Salary = objDTO.Salary;
                objFromDb.ProjectStartDate = objDTO.ProjectStartDate;
                objFromDb.VolunteersLimit = objDTO.VolunteersLimit;
                objFromDb.ProjectEndDate = objDTO.ProjectEndDate;
                objFromDb.ProjectId = objDTO.ProjectId;
                objFromDb.IsApproved = objDTO.IsApproved;
                _db.ProjectDates.Update(objFromDb);
                await _db.SaveChangesAsync();
                return _mapper.Map<ProjectDate, ProjectDateDTO>(objFromDb);
            }
            return objDTO;

        }
    }
}
