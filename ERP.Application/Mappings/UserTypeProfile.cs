using AutoMapper;
using ERP.Domain.DTOs.Auth;
using ERP.Domain.Entities.Auth;

namespace ERP.Application.Mappings
{
    public class UserTypeProfile : Profile
    {
        public UserTypeProfile()
        {
            // Entity to DTO mappings
            CreateMap<UserTypes, UserTypeDto>();

            CreateMap<UserTypes, UserTypeSummaryDto>()
                .ForMember(dest => dest.UserCount, opt => opt.MapFrom(src => src.Users.Count));

            CreateMap<UserTypes, UserTypeListResponseDto>()
                .ForMember(dest => dest.UserCount, opt => opt.MapFrom(src => src.Users.Count));

            // DTO to Entity mappings
            CreateMap<CreateUserTypeDto, UserTypes>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()));
                // AutoMapper mapea automáticamente: Name, Description, Status
                // AutoMapper ignora automáticamente: Users

            CreateMap<UpdateUserTypeDto, UserTypes>();
                // AutoMapper mapea automáticamente: Name, Description, Status
                // AutoMapper ignora automáticamente: Id, Users
        }
    }
}
