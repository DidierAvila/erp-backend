using AutoMapper;
using ERP.Domain.DTOs.Auth;
using ERP.Domain.Entities.Auth;

namespace ERP.Application.Mappings
{
    public class PermissionProfile : Profile
    {
        public PermissionProfile()
        {
            // Entity to DTO mappings
            CreateMap<Permission, PermissionDto>();

            CreateMap<Permission, PermissionSummaryDto>()
                .ForMember(dest => dest.RoleCount, opt => opt.MapFrom(src => src.Roles.Count));

            CreateMap<Permission, PermissionListResponseDto>()
                .ForMember(dest => dest.RoleCount, opt => opt.MapFrom(src => src.Roles.Count));

            CreateMap<Permission, PermissionWithRolesDto>()
                .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.Roles));

            CreateMap<Permission, PermissionDropdownDto>();

            // DTO to Entity mappings
            CreateMap<CreatePermissionDto, Permission>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.RolePermissions, opt => opt.Ignore())
                .ForMember(dest => dest.Roles, opt => opt.Ignore());
                // AutoMapper mapea automáticamente: Name, Description

            CreateMap<UpdatePermissionDto, Permission>()
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.RolePermissions, opt => opt.Ignore())
                .ForMember(dest => dest.Roles, opt => opt.Ignore());
                // AutoMapper mapea automáticamente: Name, Description
                // AutoMapper ignora automáticamente: Id, CreatedAt, Roles
        }
    }
}
