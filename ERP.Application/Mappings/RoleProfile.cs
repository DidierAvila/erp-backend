using AutoMapper;
using ERP.Domain.DTOs.Auth;
using ERP.Domain.Entities.Auth;

namespace ERP.Application.Mappings
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            // Entity to DTO mappings
            CreateMap<Role, RoleDto>()
                .ForMember(dest => dest.Permissions, opt => opt.MapFrom(src => src.Permissions));

            CreateMap<Role, RoleSummaryDto>()
                .ForMember(dest => dest.PermissionCount, opt => opt.MapFrom(src => src.Permissions.Count))
                .ForMember(dest => dest.UserCount, opt => opt.MapFrom(src => src.Users.Count));

            CreateMap<Role, RoleListResponseDto>()
                .ForMember(dest => dest.UserCount, opt => opt.MapFrom(src => src.Users.Count))
                .ForMember(dest => dest.PermissionCount, opt => opt.MapFrom(src => src.Permissions.Count));

            CreateMap<Role, RoleWithDetailsDto>()
                .ForMember(dest => dest.Permissions, opt => opt.MapFrom(src => src.Permissions))
                .ForMember(dest => dest.Users, opt => opt.MapFrom(src => src.Users));

            // DTO to Entity mappings
            CreateMap<CreateRoleDto, Role>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.RolePermissions, opt => opt.Ignore())
                .ForMember(dest => dest.Users, opt => opt.Ignore())
                .ForMember(dest => dest.Permissions, opt => opt.Ignore());
                // AutoMapper mapea automáticamente: Name, Description

            CreateMap<UpdateRoleDto, Role>()
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.RolePermissions, opt => opt.Ignore())
                .ForMember(dest => dest.Users, opt => opt.Ignore())
                .ForMember(dest => dest.Permissions, opt => opt.Ignore());
                // AutoMapper mapea automáticamente: Name, Description
        }
    }
}
