using AutoMapper;
using ERP.Domain.DTOs.Common;
using ERP.Domain.DTOs.Inventory;
using ERP.Domain.Entities.Inventory;
using ERP.Domain.Repositories;

namespace ERP.Application.Core.Inventory.Queries.Products
{
    public class GetAllProducts
    {
        private readonly IRepositoryBase<Product> _productRepository;
        private readonly IMapper _mapper;

        public GetAllProducts(IRepositoryBase<Product> productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductDto>> HandleAsync(CancellationToken cancellationToken)
        {
            var products = await _productRepository.GetAll(cancellationToken);

            // Map Entities to DTOs using AutoMapper
            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }

        public async Task<PaginationResponseDto<ProductDto>> HandleAsync(ProductFilterDto filter, CancellationToken cancellationToken = default)
        {
            // Validar y establecer valores por defecto
            if (filter.Page <= 0) filter.Page = 1;
            if (filter.PageSize <= 0) filter.PageSize = 10;
            if (filter.PageSize > 100) filter.PageSize = 100;

            // Obtener todos los productos
            var allProducts = await _productRepository.GetAll(cancellationToken);
            var query = allProducts.AsQueryable();

            // Aplicar filtros
            query = ApplyFilters(query, filter);

            // Contar total de registros
            var totalRecords = query.Count();

            // Aplicar ordenamiento
            query = ApplySorting(query, filter.SortBy);

            // Aplicar paginación
            var products = query
                .Skip((filter.Page - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .ToList();

            // Mapear a DTOs
            var productDtos = _mapper.Map<List<ProductDto>>(products);

            return productDtos.ToPaginatedResult(
                filter.Page, 
                filter.PageSize, 
                totalRecords, 
                filter.SortBy);
        }

        // Método sobrecargado para mantener compatibilidad
        public async Task<PaginationResponseDto<ProductDto>> HandleAsync(int page = 1, int pageSize = 10, string? sortBy = null, string? productName = null, CancellationToken cancellationToken = default)
        {
            var filter = new ProductFilterDto
            {
                Page = page,
                PageSize = pageSize,
                SortBy = sortBy,
                ProductName = productName
            };
            
            return await HandleAsync(filter, cancellationToken);
        }

        private IQueryable<Product> ApplyFilters(IQueryable<Product> query, ProductFilterDto filter)
        {
            // Filtro por búsqueda general (nombre, SKU y descripción)
            if (!string.IsNullOrWhiteSpace(filter.Search))
            {
                var searchTerm = filter.Search.ToLower();
                query = query.Where(p => 
                    p.ProductName.ToLower().Contains(searchTerm) ||
                    p.Sku.ToLower().Contains(searchTerm) ||
                    (p.Description != null && p.Description.ToLower().Contains(searchTerm)));
            }

            // Filtro por nombre específico
            if (!string.IsNullOrWhiteSpace(filter.ProductName))
            {
                query = query.Where(p => p.ProductName.ToLower().Contains(filter.ProductName.ToLower()));
            }

            // Filtro por SKU específico
            if (!string.IsNullOrWhiteSpace(filter.Sku))
            {
                query = query.Where(p => p.Sku.ToLower().Contains(filter.Sku.ToLower()));
            }

            // Filtro por descripción
            if (!string.IsNullOrWhiteSpace(filter.Description))
            {
                query = query.Where(p => p.Description != null && p.Description.ToLower().Contains(filter.Description.ToLower()));
            }

            // Filtro por unidad de medida
            if (!string.IsNullOrWhiteSpace(filter.UnitOfMeasure))
            {
                query = query.Where(p => p.UnitOfMeasure.ToLower().Contains(filter.UnitOfMeasure.ToLower()));
            }

            // Filtros por stock actual
            if (filter.MinCurrentStock.HasValue)
            {
                query = query.Where(p => p.CurrentStock >= filter.MinCurrentStock.Value);
            }

            if (filter.MaxCurrentStock.HasValue)
            {
                query = query.Where(p => p.CurrentStock <= filter.MaxCurrentStock.Value);
            }

            // Filtro por stock bajo
            if (filter.IsLowStock.HasValue && filter.IsLowStock.Value)
            {
                // Asumiendo que existe una propiedad MinimumStock o un valor por defecto
                // Por ahora usaremos un valor fijo de 10 como stock mínimo
                query = query.Where(p => p.CurrentStock <= 10);
            }

            return query;
        }

        private IQueryable<Product> ApplySorting(IQueryable<Product> query, string? sortBy)
        {
            return (sortBy?.ToLower()) switch
            {
                "name" or "productname" => query.OrderBy(p => p.ProductName),
                "sku" => query.OrderBy(p => p.Sku),
                "description" => query.OrderBy(p => p.Description),
                "unitofmeasure" => query.OrderBy(p => p.UnitOfMeasure),
                "currentstock" => query.OrderBy(p => p.CurrentStock),
                "id" => query.OrderBy(p => p.Id),
                _ => query.OrderBy(p => p.Id)
            };
        }
    }
}
