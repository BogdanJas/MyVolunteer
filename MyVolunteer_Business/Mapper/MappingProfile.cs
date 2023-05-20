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
            CreateMap<ProjectSignUpDetail, ProjectSignUpDetailDTO>().ReverseMap();
            CreateMap<ProjectSignUpHeader, ProjectSignUpHeaderDTO>().ReverseMap();
            CreateMap<ProjectSignUp, ProjectSignUpDTO>().ReverseMap();
            CreateMap<Volunteer, VolunteerDTO>().ReverseMap();
        }
    }
}
