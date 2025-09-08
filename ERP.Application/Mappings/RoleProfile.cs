using AutoMapper;
using ERP.Domain.DTOs.Auth;
using ERP.Domain.Entities;

namespace ERP.Application.Mappings
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            // Entity to DTO mappings
            CreateMap<Role, RoleDto>();

            // DTO to Entity mappings
            CreateMap<CreateRoleDto, Role>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()));
                // AutoMapper mapea automáticamente: Name
                // AutoMapper ignora automáticamente: Permissions, Users

            CreateMap<UpdateRoleDto, Role>();
                // AutoMapper mapea automáticamente: Name
                // AutoMapper ignora automáticamente: Id, Permissions, Users
        }
    }
}
