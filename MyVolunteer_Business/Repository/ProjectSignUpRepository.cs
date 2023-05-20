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

        public ProjectSignUpRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<ProjectSignUpDTO> Create(ProjectSignUpDTO body)
        {
            try
            {
                var obj = _mapper.Map<ProjectSignUpDTO, ProjectSignUp>(body);
                _db.ProjectSignUpHeaders.Add(obj.ProjectSignUpHeader);
                await _db.SaveChangesAsync();

                foreach (var details in obj.ProjectSignUpDetails)
                {
                    details.ProjectSignUpHeaderId = obj.ProjectSignUpHeader.Id;
                }
                _db.ProjectSignUpDetails.AddRange(obj.ProjectSignUpDetails);
                await _db.SaveChangesAsync();

                return new ProjectSignUpDTO()
                {
                    ProjectSignUpHeader = _mapper.Map<ProjectSignUpHeader, ProjectSignUpHeaderDTO>(obj.ProjectSignUpHeader),
                    ProjectSignUpDetail = _mapper.Map<IEnumerable<ProjectSignUpDetail>, IEnumerable<ProjectSignUpDetailDTO>>(obj.ProjectSignUpDetails).ToList()
                };

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return body;
        }

        public async Task<int> Delete(int Id)
        {
            var objHeader = await _db.ProjectSignUpHeaders.FirstOrDefaultAsync(u => u.Id == Id);
            if (objHeader != null)
            {
                IEnumerable<ProjectSignUpDetail> objDetail = _db.ProjectSignUpDetails.Where(u => u.ProjectSignUpHeaderId == Id);

                _db.ProjectSignUpDetails.RemoveRange(objDetail);
                _db.ProjectSignUpHeaders.Remove(objHeader);
                return _db.SaveChanges();
            }
            return 0;

        }

        public async Task<ProjectSignUpDTO> Get(int Id)
        {
            ProjectSignUp projectSignUp = new()
            {
                ProjectSignUpHeader = _db.ProjectSignUpHeaders.FirstOrDefault(u => u.Id == Id),
                ProjectSignUpDetails = _db.ProjectSignUpDetails.Where(u => u.ProjectSignUpHeaderId == Id).ToList(),
            };
            if(projectSignUp != null)
            {
                return _mapper.Map<ProjectSignUp,ProjectSignUpDTO>(projectSignUp);
            }
            return new ProjectSignUpDTO();
        }

        public async Task<IEnumerable<ProjectSignUpDTO>> GetAll(string? userId = null)
        {
            List<ProjectSignUp> projectSignUpFromDb = new List<ProjectSignUp>();
            IEnumerable<ProjectSignUpHeader> projectSignUpHeaderList = _db.ProjectSignUpHeaders;
            IEnumerable<ProjectSignUpDetail> projectSignUpDetailList = _db.ProjectSignUpDetails;

            foreach (ProjectSignUpHeader header in projectSignUpHeaderList)
            {
                ProjectSignUp projectSignUp = new()
                {
                    ProjectSignUpHeader = header,
                    ProjectSignUpDetails = projectSignUpDetailList.Where(u => u.ProjectSignUpHeaderId == header.Id).ToList(),
                };
                projectSignUpFromDb.Add(projectSignUp); 
            }
            //do some filtering #TODO

            return _mapper.Map<IEnumerable<ProjectSignUp>, IEnumerable<ProjectSignUpDTO>>(projectSignUpFromDb);
        }

        public async Task<ProjectSignUpHeaderDTO> UpdateHeader(ProjectSignUpHeaderDTO body)
        {
            if (body != null)
            {
                var projectSignUpHeaderFromDb = _db.ProjectSignUpHeaders.FirstOrDefault(u => u.Id == body.Id);
                projectSignUpHeaderFromDb.VolunteerId = body.VolunteerId;
                projectSignUpHeaderFromDb.SignDate = body.SignDate;

                await _db.SaveChangesAsync();
                return _mapper.Map<ProjectSignUpHeader, ProjectSignUpHeaderDTO>(projectSignUpHeaderFromDb);
            }
            return new ProjectSignUpHeaderDTO();
        }
    }
}
