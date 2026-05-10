using AutoMapper;
using Core.DTOs.AdminDtos;
using Core.Entities;

namespace Business.Mapping
{
    public class AdminProfile : Profile
    {
        public AdminProfile()
        {
            CreateMap<User, UserListDto>()
                .ForMember(d => d.PersonnelId, o => o.MapFrom(s => s.Personnel.Id))
                .ForMember(d => d.FirstName, o => o.MapFrom(s => s.Personnel.FirstName))
                .ForMember(d => d.LastName, o => o.MapFrom(s => s.Personnel.LastName))
                .ForMember(d => d.Status, o => o.MapFrom(s => s.Personnel.Status))
                .ForMember(d => d.RoleName, o => o.MapFrom(s => s.Role.Name))
                .ForMember(d => d.RoleId, o => o.MapFrom(s => s.RoleId));

            CreateMap<User, UserDetailDto>()
                .ForMember(d => d.PersonnelId, o => o.MapFrom(s => s.Personnel.Id))
                .ForMember(d => d.FirstName, o => o.MapFrom(s => s.Personnel.FirstName))
                .ForMember(d => d.LastName, o => o.MapFrom(s => s.Personnel.LastName))
                .ForMember(d => d.Status, o => o.MapFrom(s => s.Personnel.Status))
                .ForMember(d => d.RoleName, o => o.MapFrom(s => s.Role.Name))
                .ForMember(d => d.RoleId, o => o.MapFrom(s => s.RoleId))
                .ForMember(d => d.UpdatedAt, o => o.MapFrom(s => s.Personnel.UpdatedAt));
        }
    }
}
