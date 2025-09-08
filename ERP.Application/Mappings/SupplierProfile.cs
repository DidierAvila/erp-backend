using AutoMapper;
using ERP.Domain.DTOs.Purchases;
using ERP.Domain.Entities.Purchases;

namespace ERP.Application.Mappings
{
    public class SupplierProfile : Profile
    {
        public SupplierProfile()
        {
            // Entity to DTO mappings
            CreateMap<Supplier, SupplierDto>();

            // DTO to Entity mappings for creation
            CreateMap<CreateSupplierDto, Supplier>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()) // ID is auto-generated
                .ForMember(dest => dest.PurchaseOrders, opt => opt.Ignore()); // Navigation property

            // DTO to Entity mappings for updates
            CreateMap<UpdateSupplierDto, Supplier>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()) // Don't update ID
                .ForMember(dest => dest.PurchaseOrders, opt => opt.Ignore()); // Navigation property
        }
    }
}
