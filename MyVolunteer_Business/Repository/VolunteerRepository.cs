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
    public class VolunteerRepository : IVolunteerRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public VolunteerRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        public async Task<VolunteerDTO> Create(VolunteerDTO objDTO)
        {
            var obj = _mapper.Map<VolunteerDTO, Volunteer>(objDTO);
            var addedObj = _db.Volunteers.Add(obj);
            await _db.SaveChangesAsync();

            return _mapper.Map<Volunteer, VolunteerDTO>(addedObj.Entity);
        }

        public async Task<int> Delete(int id)
        {
            var obj = _db.Volunteers.FirstOrDefault(u => u.Id == id);
            if(obj!=null)
            {
                _db.Volunteers.Remove(obj);
                return await _db.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<VolunteerDTO> Get(int id)
        {
            var obj = await _db.Volunteers.FirstOrDefaultAsync(u => u.Id == id);
            if (obj != null)
            {
                return _mapper.Map<Volunteer, VolunteerDTO>(obj);
            }
            return new VolunteerDTO();
        }

        public async Task<IEnumerable<VolunteerDTO>> GetAll()
        {
            return _mapper.Map<IEnumerable<Volunteer>, IEnumerable<VolunteerDTO>>(_db.Volunteers);
        }

        public async Task<VolunteerDTO> Update(VolunteerDTO objDTO)
        {
            var objFromDb = await _db.Volunteers.FirstOrDefaultAsync(u => u.Id == objDTO.Id);
            if(objFromDb != null )
            {
                objFromDb.Name = objDTO.Name;
                objFromDb.PhoneNumber = objDTO.PhoneNumber;
                objFromDb.StreetAddress = objDTO.StreetAddress;
                objFromDb.State = objDTO.State;
                objFromDb.City = objDTO.City;
                objFromDb.PostalCode = objDTO.PostalCode;
                objFromDb.Email = objDTO.Email;
                objFromDb.Password = objDTO.Password;
                objFromDb.ImageUrl = objFromDb.ImageUrl;
                objFromDb.ResumeUrl = objDTO.ResumeUrl;

                _db.Volunteers.Update(objFromDb);
                await _db.SaveChangesAsync();
                return _mapper.Map<Volunteer, VolunteerDTO>(objFromDb);
            }
            return objDTO;
        }
    }
}
