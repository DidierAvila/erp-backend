using AutoMapper;
using ERP.Domain.DTOs.Auth;
using ERP.Domain.Entities;
using ERP.Domain.Entities.Auth;

namespace ERP.Application.Mappings
{
    public class AuthProfile : Profile
    {
        public AuthProfile()
        {
            // AutoMapper mapea automáticamente propiedades con nombres iguales
            CreateMap<Role, RoleDto>();
            CreateMap<Permission, PermissionDto>();
            CreateMap<Session, SessionDto>();
            CreateMap<Account, AccountDto>();
            
            // Mapeo personalizado para UserRole -> UserRoleDto
            // Mapea usando la navigation property Role
            CreateMap<UserRole, UserRoleDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Role.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Role.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Role.Description))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Role.Status));
        }
    }
}
