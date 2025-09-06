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
            CreateMap<User, UserDto>();

            // DTO to Entity mappings
            CreateMap<CreateUserDto, User>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow));
                // AutoMapper mapea automáticamente: Email, Password, Image, Phone, TypeUser
                // AutoMapper ignora automáticamente: Accounts, Advisor, Assistant, Customer, Sessions, Roles

            CreateMap<UpdateUserDto, User>()
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow));
                // AutoMapper mapea automáticamente: Image, Phone, TypeUser
                // AutoMapper ignora automáticamente: Id, Email, Password, CreatedAt, Accounts, etc.
        }
    }
}
