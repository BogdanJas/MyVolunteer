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
    public class ProjectSignUpRepository : IProjectSignUpRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        public readonly IProjectDateRepository _projectDateRepository;

        public ProjectSignUpRepository(ApplicationDbContext db, IMapper mapper, IProjectDateRepository projectDateRepository)
        {
            _db = db;
            _mapper = mapper;
            _projectDateRepository = projectDateRepository;
        }

        public async Task<ProjectSignUpDTO> Create(ProjectSignUpDTO body)
        {
            try
            {
                var obj = _mapper.Map<ProjectSignUpDTO, ProjectSignUp>(body);
                var addedobj = _db.ProjectSignUps.Add(obj);
                var project = await _projectDateRepository.Get(body.ProjectId);
                project.VolunteersLimit--;
                await _projectDateRepository.Update(project);
                await _db.SaveChangesAsync();

                return _mapper.Map<ProjectSignUp, ProjectSignUpDTO>(addedobj.Entity);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return body;
        }

        public async Task<int> Delete(int Id)
        {
            var obj = await _db.ProjectSignUps.FirstOrDefaultAsync(u => u.Id == Id);
            if (obj != null)
            {
                _db.ProjectSignUps.Remove(obj);
                return _db.SaveChanges();
            }
            return 0;

        }

        public async Task<ProjectSignUpDTO> Get(int Id)
        {
            var obj = await _db.ProjectSignUps.FirstOrDefaultAsync(u => u.Id == Id);
            return _mapper.Map<ProjectSignUp, ProjectSignUpDTO>(obj);
        }

        public async Task<IEnumerable<ProjectSignUpDTO>> GetAll(string? userId = null)
        {
            return _mapper.Map<IEnumerable<ProjectSignUp>, IEnumerable<ProjectSignUpDTO>>(_db.ProjectSignUps);
        }
        
    }
}
