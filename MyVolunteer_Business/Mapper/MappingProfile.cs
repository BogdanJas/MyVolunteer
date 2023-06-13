using AutoMapper;
using Microsoft.AspNetCore.Identity;
using MyVolunteer_DataAccess;
using MyVolunteer_Models;

namespace MyVolunteer_Business.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile(){
            CreateMap<Category,CategoryDTO>().ReverseMap();
            CreateMap<Project, ProjectDTO>().ReverseMap();
            CreateMap<ProjectDate, ProjectDateDTO>().ReverseMap();
            CreateMap<ProjectSignUp, ProjectSignUpDTO>()
                .ForMember(m => m.ProjectName, s => s.MapFrom(p => p.ProjectDate.Project.Name))
                .ForMember(m => m.ProjectStartDate, s => s.MapFrom(p => p.ProjectDate.ProjectStartDate))
                .ForMember(m => m.ProjectEndDate, s => s.MapFrom(p => p.ProjectDate.ProjectEndDate))
                .ForMember(m => m.ProjectPlace, s => s.MapFrom(p => p.ProjectDate.Place))
                .ForMember(m => m.VolunteerName, s => s.MapFrom(p => p.Volunteer.Name))
                .ForMember(m => m.VolunteerEmail, s => s.MapFrom(p => p.Volunteer.Email))
                .ForMember(m => m.VolunteerPhoneNumber, s => s.MapFrom(p => p.Volunteer.PhoneNumber))
                .ForMember(m => m.VolunteerDateOfBirth, s => s.MapFrom(p => p.Volunteer.DateOfBirth))
                .ForMember(m => m.VolunteerSex, s => s.MapFrom(p => p.Volunteer.Sex));

            CreateMap<ProjectSignUpDTO, ProjectSignUp>();
            CreateMap<Volunteer, VolunteerDTO>().ReverseMap();
        }
    }
}
