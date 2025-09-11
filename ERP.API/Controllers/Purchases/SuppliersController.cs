using ERP.Application.Core.Purchases.Commands.Handlers;
using ERP.Application.Core.Purchases.Queries.Handlers;
using ERP.Domain.DTOs.Purchases;
using Microsoft.AspNetCore.Mvc;

namespace ERP.API.Controllers.Purchases
{
    /// <summary>
    /// Controlador para gestionar proveedores.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class SuppliersController : ControllerBase
    {
        private readonly ISupplierCommandHandler _commandHandler;
        private readonly ISupplierQueryHandler _queryHandler;
        private readonly ILogger<SuppliersController> _logger;

        /// <summary>
        /// Constructor del controlador SuppliersController
        /// </summary>
        /// <param name="commandHandler"></param>
        /// <param name="queryHandler"></param>
        /// <param name="logger"></param>
        public SuppliersController(
            ISupplierCommandHandler commandHandler,
            ISupplierQueryHandler queryHandler,
            ILogger<SuppliersController> logger)
        {
            _commandHandler = commandHandler;
            _queryHandler = queryHandler;
            _logger = logger;
        }

        /// <summary>
        /// Crea un nuevo proveedor
        /// </summary>
        /// <param name="createSupplierDto"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(SupplierDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<SupplierDto>> CreateSupplier([FromBody] CreateSupplierDto createSupplierDto)
        {
            try
            {
                var result = await _commandHandler.CreateSupplier(createSupplierDto, CancellationToken.None);
                return CreatedAtAction(nameof(GetSupplierById), new { id = result.Id }, result);
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, "Invalid supplier data: {Message}", ex.Message);
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, "Supplier creation failed: {Message}", ex.Message);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating supplier");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        /// <summary>
        /// Actualiza un proveedor existente
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateSupplierDto"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(SupplierDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<SupplierDto>> UpdateSupplier(int id, [FromBody] UpdateSupplierDto updateSupplierDto)
        {
            try
            {
                var result = await _commandHandler.UpdateSupplier(id, updateSupplierDto, CancellationToken.None);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, "Invalid supplier data: {Message}", ex.Message);
                return BadRequest(ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "Supplier not found: {Message}", ex.Message);
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, "Supplier update failed: {Message}", ex.Message);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating supplier with ID {SupplierId}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        /// <summary>
        /// Elimina un proveedor por ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteSupplier(int id)
        {
            try
            {
                var result = await _commandHandler.DeleteSupplier(id, CancellationToken.None);

                if (!result)
                {
                    return NotFound($"Supplier with ID {id} not found");
                }

                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, "Supplier deletion failed: {Message}", ex.Message);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting supplier with ID {SupplierId}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        /// <summary>
        /// Obtiene un proveedor por ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(SupplierDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<SupplierDto>> GetSupplierById(int id)
        {
            try
            {
                var result = await _queryHandler.GetSupplierById(id, CancellationToken.None);
                
                if (result == null)
                {
                    return NotFound($"Supplier with ID {id} not found");
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting supplier with ID {SupplierId}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        /// <summary>
        /// Obtiene todos los proveedores
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<SupplierDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<SupplierDto>>> GetAllSuppliers()
        {
            try
            {
                var result = await _queryHandler.GetAllSuppliers(CancellationToken.None);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all suppliers");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        /// <summary>
        /// Busca proveedores por nombre (parcial o completo)
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet("search")]
        [ProducesResponseType(typeof(IEnumerable<SupplierDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<SupplierDto>>> GetSuppliersByName([FromQuery] string? name)
        {
            try
            {
                var result = await _queryHandler.GetSuppliersByName(name ?? string.Empty, CancellationToken.None);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error searching suppliers by name: {Name}", name);
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }
    }
}
