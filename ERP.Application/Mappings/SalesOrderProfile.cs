using AutoMapper;
using ERP.Domain.DTOs.Sales;
using ERP.Domain.Entities.Sales;

namespace ERP.Application.Mappings
{
    public class SalesOrderProfile : Profile
    {
        public SalesOrderProfile()
        {
            // Entity to DTO mappings
            CreateMap<SalesOrder, SalesOrderDto>()
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.SalesOrderItems));

            // SalesOrderItem to SalesOrderItemDto mapping
            CreateMap<SalesOrderItem, SalesOrderItemDto>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product != null ? src.Product.ProductName : null))
                .ForMember(dest => dest.Subtotal, opt => opt.MapFrom(src => src.Quantity * src.UnitPrice));

            // DTO to Entity mappings for creation
            CreateMap<CreateSalesOrderDto, SalesOrder>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()) // ID is auto-generated
                .ForMember(dest => dest.OrderDate, opt => opt.MapFrom(src => DateOnly.FromDateTime(DateTime.Now)))
                .ForMember(dest => dest.TotalAmount, opt => opt.Ignore()) // Calculated in command
                .ForMember(dest => dest.Invoices, opt => opt.Ignore()) // Navigation property
                .ForMember(dest => dest.SalesOrderItems, opt => opt.Ignore()); // Handled manually in command

            // DTO to Entity mappings for SalesOrderItems
            CreateMap<CreateSalesOrderItemDto, SalesOrderItem>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.SalesOrderId, opt => opt.Ignore())
                .ForMember(dest => dest.Product, opt => opt.Ignore())
                .ForMember(dest => dest.SalesOrder, opt => opt.Ignore());

            // Summary DTO mapping
            CreateMap<SalesOrder, SalesOrderSummaryDto>()
                .ForMember(dest => dest.ItemCount, opt => opt.MapFrom(src => src.SalesOrderItems != null ? src.SalesOrderItems.Count : 0));
        }
    }
}
