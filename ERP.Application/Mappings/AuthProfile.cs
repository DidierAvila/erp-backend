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
        }
    }
}
