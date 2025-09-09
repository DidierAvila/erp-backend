using AutoMapper;
using ERP.Domain.DTOs.Auth;
using ERP.Domain.Entities.Auth;

namespace ERP.Application.Mappings
{
    public class UserTypesProfile : Profile
    {
        public UserTypesProfile()
        {
            // Entity to DTO mappings
            CreateMap<UserTypes, UserTypesDto>();

            // DTO to Entity mappings
            CreateMap<CreateUserTypesDto, UserTypes>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()));
                // AutoMapper mapea automáticamente: Name, Description, Status
                // AutoMapper ignora automáticamente: Users

            CreateMap<UpdateUserTypesDto, UserTypes>();
                // AutoMapper mapea automáticamente: Name, Description, Status
                // AutoMapper ignora automáticamente: Id, Users
        }
    }
}
