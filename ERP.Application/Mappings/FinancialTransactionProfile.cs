using AutoMapper;
using ERP.Domain.DTOs.Finance;
using ERP.Domain.Entities.Finance;

namespace ERP.Application.Mappings
{
    public class FinancialTransactionProfile : Profile
    {
        public FinancialTransactionProfile()
        {
            // Entity to DTO mappings
            CreateMap<FinancialTransaction, FinancialTransactionDto>()
                .ForMember(dest => dest.AccountType, opt => opt.MapFrom(src => src.Account != null ? src.Account.Type : null));

            // DTO to Entity mappings
            CreateMap<CreateFinancialTransactionDto, FinancialTransaction>();
                // AutoMapper mapea automáticamente: TransactionType, Amount, TransactionDate, Description, AccountId
                // AutoMapper ignora automáticamente: Id, CreatedAt, Account

            CreateMap<UpdateFinancialTransactionDto, FinancialTransaction>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
                // Solo mapea propiedades que no son null
                // AutoMapper ignora automáticamente: Id, CreatedAt, Account
        }
    }
}
