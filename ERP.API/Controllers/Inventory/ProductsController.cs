using ERP.Application.Core.Inventory.Commands.Handlers;
using ERP.Application.Core.Inventory.Queries.Handlers;
using ERP.Domain.DTOs.Inventory;
using Microsoft.AspNetCore.Mvc;

namespace ERP.API.Controllers.Inventory
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductCommandHandler _productCommandHandler;
        private readonly IProductQueryHandler _productQueryHandler;

        public ProductsController(IProductCommandHandler productCommandHandler, IProductQueryHandler productQueryHandler)
        {
            _productCommandHandler = productCommandHandler;
            _productQueryHandler = productQueryHandler;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetAllProducts(CancellationToken cancellationToken)
        {
            try
            {
                var products = await _productQueryHandler.GetAllProducts(cancellationToken);
                return Ok(products);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/Products/{id}
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

        // GET: api/Products/by-sku/{sku}
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

        // GET: api/Products/low-stock
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

        // POST: api/Products
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

        // PUT: api/Products/{id}
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

        // DELETE: api/Products/{id}
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
