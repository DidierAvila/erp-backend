using AutoMapper;
using ERP.Domain.DTOs.Auth;
using ERP.Domain.Entities.Auth;

namespace ERP.Application.Mappings
{
    public class RolePermissionProfile : Profile
    {
        public RolePermissionProfile()
        {
            // Entity to DTO
            CreateMap<RolePermission, RolePermissionDto>()
                .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Role.Name))
                .ForMember(dest => dest.PermissionName, opt => opt.MapFrom(src => src.Permission.Name));

            // DTO to Entity
            CreateMap<CreateRolePermissionDto, RolePermission>()
                .ForMember(dest => dest.Role, opt => opt.Ignore())
                .ForMember(dest => dest.Permission, opt => opt.Ignore());

            CreateMap<AssignPermissionToRoleDto, RolePermission>()
                .ForMember(dest => dest.Role, opt => opt.Ignore())
                .ForMember(dest => dest.Permission, opt => opt.Ignore());
        }
    }
}
