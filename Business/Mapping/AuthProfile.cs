using AutoMapper;
using Core.DTOs.AuthDtos;
using Core.Entities;

namespace Business.Mapping
{
    public class AuthProfile : Profile
    {
        public AuthProfile()
        {
            CreateMap<User, AuthenticationDto>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.Personnel.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.Personnel.LastName))
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role.Name))
                .ForMember(dest => dest.PersonnelId, opt => opt.MapFrom(src => src.Personnel.Id));
        }
    }
}