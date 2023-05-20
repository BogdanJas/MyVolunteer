using AutoMapper;
using Microsoft.EntityFrameworkCore;
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
    public class ProjectRepository : IProjectRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public ProjectRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<ProjectDTO> Create(ProjectDTO objDTO)
        {
            var obj = _mapper.Map<ProjectDTO, Project>(objDTO);
            var addedObj = _db.Projects.Add(obj);
            await _db.SaveChangesAsync();

            return _mapper.Map<Project, ProjectDTO>(addedObj.Entity);
        }

        public async Task<int> Delete(int id)
        {
            var obj = await _db.Projects.FirstOrDefaultAsync(u => u.Id == id);
            if (obj != null)
            {
                _db.Projects.Remove(obj);
                return await _db.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<ProjectDTO> Get(int id)
        {
            var obj = await _db.Projects.Include(u => u.Category).Include(u => u.ProjectDates).FirstOrDefaultAsync(u => u.Id == id);
            if (obj != null)
            {
                return _mapper.Map<Project, ProjectDTO>(obj);
            }
            return new ProjectDTO();
        }

        public async Task<IEnumerable<ProjectDTO>> GetAll()
        {
            return _mapper.Map<IEnumerable<Project>, IEnumerable<ProjectDTO>>(_db.Projects.Include(u => u.Category).Include(u => u.ProjectDates));
        }

        public async Task<ProjectDTO> Update(ProjectDTO objDTO)
        {
            var objFromDb = await _db.Projects.FirstOrDefaultAsync(u => u.Id == objDTO.Id);
            if (objFromDb != null)
            {
                objFromDb.Name = objDTO.Name;
                objFromDb.Description = objDTO.Description;
                objFromDb.ImageUrl = objDTO.ImageUrl;
                objFromDb.CategoryId = objDTO.CategoryId;
                objFromDb.OrganizatonFavourite = objDTO.OrganisationFavourite;
                objFromDb.VolunteerFavourite = objDTO.VolunteerFavourite;
                _db.Projects.Update(objFromDb);
                await _db.SaveChangesAsync();
                return _mapper.Map<Project, ProjectDTO>(objFromDb);
            }
            return objDTO;

        }
    }
}

