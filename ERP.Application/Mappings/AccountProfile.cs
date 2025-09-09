using AutoMapper;
using ERP.Domain.DTOs.Finance;
using ERP.Domain.Entities.Finance;

namespace ERP.Application.Mappings
{
    public class AccountProfile : Profile
    {
        public AccountProfile()
        {
            // Entity to DTO mappings
            CreateMap<Account, AccountDto>();
            CreateMap<Account, AccountSummaryDto>();

            // DTO to Entity mappings
            CreateMap<CreateAccountDto, Account>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow));
                // AutoMapper mapea automáticamente: AccountName, AccountNumber, AccountType, Description, Balance, IsActive
                // AutoMapper ignora automáticamente: FinancialTransactions

            CreateMap<UpdateAccountDto, Account>()
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
                // Solo actualiza los campos que no son null
                // AutoMapper ignora automáticamente: Id, CreatedAt, FinancialTransactions
        }
    }
}
