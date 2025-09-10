using AutoMapper;
using ERP.Domain.DTOs.Auth;
using ERP.Domain.Entities.Auth;
using System.Text.Json;

namespace ERP.Application.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            // Entity to DTO mappings
            CreateMap<User, UserDto>()
                .ForMember(dest => dest.AdditionalData, opt => opt.Ignore())
                .ForMember(dest => dest.UserTypeName, opt => opt.Ignore()) // Se asignará manualmente
                .AfterMap((src, dest) => {
                    if (!string.IsNullOrEmpty(src.ExtraData) && src.ExtraData != "{}")
                    {
                        try
                        {
                            dest.AdditionalData = JsonSerializer.Deserialize<Dictionary<string, object>>(src.ExtraData);
                        }
                        catch
                        {
                            dest.AdditionalData = new Dictionary<string, object>();
                        }
                    }
                });

            CreateMap<User, UserWithDetailsDto>()
                .ForMember(dest => dest.AdditionalData, opt => opt.Ignore())
                .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.Roles))
                .ForMember(dest => dest.UserType, opt => opt.MapFrom(src => src.UserType))
                .AfterMap((src, dest) => {
                    if (!string.IsNullOrEmpty(src.ExtraData) && src.ExtraData != "{}")
                    {
                        try
                        {
                            dest.AdditionalData = JsonSerializer.Deserialize<Dictionary<string, object>>(src.ExtraData);
                        }
                        catch
                        {
                            dest.AdditionalData = new Dictionary<string, object>();
                        }
                    }
                });

            CreateMap<User, UserSummaryDto>()
                .ForMember(dest => dest.UserTypeName, opt => opt.MapFrom(src => src.UserType.Name))
                .ForMember(dest => dest.RoleCount, opt => opt.MapFrom(src => src.Roles.Count))
                .ForMember(dest => dest.LastLoginAt, opt => opt.Ignore()); // Se puede mapear desde Sessions si es necesario

            // DTO to Entity mappings
            CreateMap<CreateUserDto, User>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.ExtraData, opt => opt.MapFrom(src => "{}"))
                .ForMember(dest => dest.Accounts, opt => opt.Ignore())
                .ForMember(dest => dest.Sessions, opt => opt.Ignore())
                .ForMember(dest => dest.Roles, opt => opt.Ignore())
                .ForMember(dest => dest.UserType, opt => opt.Ignore())
                .AfterMap((src, dest) => {
                    if (src.AdditionalData != null)
                    {
                        dest.ExtraData = JsonSerializer.Serialize(src.AdditionalData);
                    }
                });
                // AutoMapper mapea automáticamente: Email, Password, Image, Phone, UserTypeId
                // AutoMapper ignora automáticamente: Accounts, Sessions, Roles, UserType

            CreateMap<UpdateUserDto, User>()
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.Password, opt => opt.Ignore())
                .ForMember(dest => dest.Accounts, opt => opt.Ignore())
                .ForMember(dest => dest.Sessions, opt => opt.Ignore())
                .ForMember(dest => dest.Roles, opt => opt.Ignore())
                .ForMember(dest => dest.UserType, opt => opt.Ignore())
                .AfterMap((src, dest) => {
                    if (src.AdditionalData != null)
                    {
                        dest.ExtraData = JsonSerializer.Serialize(src.AdditionalData);
                    }
                });
                // AutoMapper mapea automáticamente: Image, Phone, UserTypeId
                // AutoMapper ignora automáticamente: Id, Email, Password, CreatedAt, Accounts, Sessions, Roles, UserType

            // Mapeo optimizado sin AdditionalData para listas
            CreateMap<User, UserBasicDto>()
                .ForMember(dest => dest.UserTypeName, opt => opt.Ignore()); // Se asignará manualmente
        }
    }
}
