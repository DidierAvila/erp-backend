using AutoMapper;
using ERP.Domain.DTOs.Inventory;
using ERP.Domain.Entities.Inventory;

namespace ERP.Application.Mappings
{
    public class StockMovementProfile : Profile
    {
        public StockMovementProfile()
        {
            // Entity to DTO mappings
            CreateMap<StockMovement, StockMovementDto>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product != null ? src.Product.ProductName : null))
                .ForMember(dest => dest.FromLocationName, opt => opt.MapFrom(src => src.FromLocation != null ? src.FromLocation.LocationName : null))
                .ForMember(dest => dest.ToLocationName, opt => opt.MapFrom(src => src.ToLocation != null ? src.ToLocation.LocationName : null))
                .ForMember(dest => dest.Notes, opt => opt.Ignore()); // Notes is not in entity but in DTO

            // DTO to Entity mappings
            CreateMap<CreateStockMovementDto, StockMovement>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.MovementDate, opt => opt.Ignore()) // Set in command handler
                .ForMember(dest => dest.Product, opt => opt.Ignore())
                .ForMember(dest => dest.FromLocation, opt => opt.Ignore())
                .ForMember(dest => dest.ToLocation, opt => opt.Ignore());
                // AutoMapper mapea automáticamente: MovementType, ProductId, FromLocationId, ToLocationId, Quantity

            // Report DTO mapping
            CreateMap<StockMovement, StockMovementReportDto>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product != null ? src.Product.ProductName : "Unknown"))
                .ForMember(dest => dest.TotalQuantity, opt => opt.MapFrom(src => src.Quantity))
                .ForMember(dest => dest.LastMovement, opt => opt.MapFrom(src => src.MovementDate));
        }
    }
}
