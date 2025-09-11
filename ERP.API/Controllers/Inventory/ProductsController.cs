using ERP.Application.Core.Inventory.Commands.Handlers;
using ERP.Application.Core.Inventory.Queries.Handlers;
using ERP.Domain.DTOs.Common;
using ERP.Domain.DTOs.Inventory;
using Microsoft.AspNetCore.Mvc;

namespace ERP.API.Controllers.Inventory
{
    /// <summary>
    /// Controlador para gestionar productos en el inventario.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductCommandHandler _productCommandHandler;
        private readonly IProductQueryHandler _productQueryHandler;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="ProductsController"/>.
        /// </summary>
        /// <param name="productCommandHandler"></param>
        /// <param name="productQueryHandler"></param>
        public ProductsController(IProductCommandHandler productCommandHandler, IProductQueryHandler productQueryHandler)
        {
            _productCommandHandler = productCommandHandler;
            _productQueryHandler = productQueryHandler;
        }

        /// <summary>
        /// Obtiene una lista paginada de productos con filtros opcionales
        /// </summary>
        /// <param name="filter">Filtros de búsqueda y paginación</param>
        /// <param name="cancellationToken">Token de cancelación</param>
        /// <returns>Lista paginada de productos</returns>
        /// <remarks>
        /// Campos disponibles para SortBy: name, productname, sku, description, unitofmeasure, currentstock, id
        /// 
        /// Ejemplo de uso:
        /// GET /api/Products?page=1&amp;pageSize=10&amp;search=laptop&amp;sortBy=name
        /// GET /api/Products?productName=mouse&amp;minCurrentStock=5&amp;isLowStock=true
        /// </remarks>
        [HttpGet]
        public async Task<ActionResult<PaginationResponseDto<ProductDto>>> GetAllProducts(
            [FromQuery] ProductFilterDto filter,
            CancellationToken cancellationToken = default)
        {
            try
            {
                // Si no se proporcionan parámetros de filtro específicos, usar el método original para compatibilidad
                if (IsEmptyFilter(filter))
                {
                    var allProducts = await _productQueryHandler.GetAllProducts(cancellationToken);
                    return Ok(allProducts);
                }
                
                // Usar el método con filtros avanzados
                var paginatedResult = await _productQueryHandler.GetAllProducts(filter, cancellationToken);
                return Ok(paginatedResult);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Verifica si el filtro está vacío (sin parámetros específicos)
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        private static bool IsEmptyFilter(ProductFilterDto filter)
        {
            return filter.Page <= 1 && 
                   filter.PageSize <= 10 && 
                   string.IsNullOrEmpty(filter.SortBy) &&
                   string.IsNullOrEmpty(filter.Search) &&
                   string.IsNullOrEmpty(filter.ProductName) &&
                   string.IsNullOrEmpty(filter.Sku) &&
                   string.IsNullOrEmpty(filter.Description) &&
                   string.IsNullOrEmpty(filter.UnitOfMeasure) &&
                   !filter.MinCurrentStock.HasValue &&
                   !filter.MaxCurrentStock.HasValue &&
                   !filter.MinMinimumStock.HasValue &&
                   !filter.MaxMinimumStock.HasValue &&
                   !filter.IsLowStock.HasValue;
        }

        /// <summary>
        /// Obtiene un producto por ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProductById(int id, CancellationToken cancellationToken)
        {
            try
            {
                var product = await _productQueryHandler.GetProductById(id, cancellationToken);
                if (product == null)
                    return NotFound("Product not found");

                return Ok(product);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Obtiene un producto por SKU
        /// </summary>
        /// <param name="sku"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("by-sku/{sku}")]
        public async Task<ActionResult<ProductDto>> GetProductBySku(string sku, CancellationToken cancellationToken)
        {
            try
            {
                var product = await _productQueryHandler.GetProductBySku(sku, cancellationToken);
                if (product == null)
                    return NotFound("Product not found");

                return Ok(product);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Obtiene productos con bajo stock
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("low-stock")]
        public async Task<ActionResult<IEnumerable<ProductStockDto>>> GetLowStockProducts(CancellationToken cancellationToken)
        {
            try
            {
                var products = await _productQueryHandler.GetLowStockProducts(cancellationToken);
                return Ok(products);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        /// <summary>
        /// Crea un nuevo producto
        /// </summary>
        /// <param name="createProductDto"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<ProductDto>> CreateProduct([FromBody] CreateProductDto createProductDto, CancellationToken cancellationToken)
        {
            try
            {
                var createdProduct = await _productCommandHandler.CreateProduct(createProductDto, cancellationToken);
                return CreatedAtAction(nameof(GetProductById), new { id = createdProduct.Id }, createdProduct);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Actualiza un producto existente
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateProductDto"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<ProductDto>> UpdateProduct(int id, [FromBody] UpdateProductDto updateProductDto, CancellationToken cancellationToken)
        {
            try
            {
                var updatedProduct = await _productCommandHandler.UpdateProduct(id, updateProductDto, cancellationToken);
                return Ok(updatedProduct);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Elimina un producto por ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct(int id, CancellationToken cancellationToken)
        {
            try
            {
                await _productCommandHandler.DeleteProduct(id, cancellationToken);
                return Ok();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
