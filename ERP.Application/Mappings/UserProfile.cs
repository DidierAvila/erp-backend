using AutoMapper;
using ERP.Domain.DTOs.Auth;
using ERP.Domain.Entities.Auth;

namespace ERP.Application.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            // Entity to DTO mappings
            CreateMap<User, UserDto>()
                .ForMember(dest => dest.AdditionalData, opt => opt.MapFrom(src => src.AdditionalData));

            // DTO to Entity mappings
            CreateMap<CreateUserDto, User>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.ExtraData, opt => opt.MapFrom(src => "{}"))
                .AfterMap((src, dest) => {
                    if (src.AdditionalData != null)
                        dest.AdditionalData = src.AdditionalData;
                });
                // AutoMapper mapea automáticamente: Email, Password, Image, Phone, UserTypeId
                // AutoMapper ignora automáticamente: Accounts, Sessions, Roles, UserType

            CreateMap<UpdateUserDto, User>()
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .AfterMap((src, dest) => {
                    if (src.AdditionalData != null)
                        dest.AdditionalData = src.AdditionalData;
                });
                // AutoMapper mapea automáticamente: Image, Phone, UserTypeId
                // AutoMapper ignora automáticamente: Id, Email, Password, CreatedAt, Accounts, Sessions, Roles, UserType
        }
    }
}
