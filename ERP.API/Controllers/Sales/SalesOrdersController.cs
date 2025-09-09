using ERP.Application.Core.Sales.Commands.Handlers;
using ERP.Application.Core.Sales.Queries.Handlers;
using ERP.Domain.DTOs.Sales;
using Microsoft.AspNetCore.Mvc;

namespace ERP.API.Controllers.Sales
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesOrdersController : ControllerBase
    {
        private readonly ISalesOrderCommandHandler _commandHandler;
        private readonly ISalesOrderQueryHandler _queryHandler;
        private readonly ILogger<SalesOrdersController> _logger;

        public SalesOrdersController(
            ISalesOrderCommandHandler commandHandler,
            ISalesOrderQueryHandler queryHandler,
            ILogger<SalesOrdersController> logger)
        {
            _commandHandler = commandHandler;
            _queryHandler = queryHandler;
            _logger = logger;
        }

        [HttpPost]
        [ProducesResponseType(typeof(SalesOrderDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<SalesOrderDto>> CreateSalesOrder([FromBody] CreateSalesOrderDto createSalesOrderDto)
        {
            try
            {
                var result = await _commandHandler.CreateSalesOrder(createSalesOrderDto, CancellationToken.None);
                return CreatedAtAction(nameof(GetSalesOrderById), new { id = result.Id }, result);
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning("Invalid sales order data: {Message}", ex.Message);
                return BadRequest(ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning("Related entity not found: {Message}", ex.Message);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating sales order");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(SalesOrderDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<SalesOrderDto>> UpdateSalesOrder(int id, [FromBody] UpdateSalesOrderDto updateSalesOrderDto)
        {
            try
            {
                var result = await _commandHandler.UpdateSalesOrder(id, updateSalesOrderDto, CancellationToken.None);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning("Invalid sales order data: {Message}", ex.Message);
                return BadRequest(ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning("Sales order not found: {Message}", ex.Message);
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating sales order with ID {SalesOrderId}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteSalesOrder(int id)
        {
            try
            {
                var result = await _commandHandler.DeleteSalesOrder(id, CancellationToken.None);
                
                if (!result)
                {
                    return NotFound($"Sales order with ID {id} not found");
                }

                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning("Sales order deletion failed: {Message}", ex.Message);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting sales order with ID {SalesOrderId}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(SalesOrderDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<SalesOrderDto>> GetSalesOrderById(int id)
        {
            try
            {
                var result = await _queryHandler.GetSalesOrderById(id, CancellationToken.None);
                
                if (result == null)
                {
                    return NotFound($"Sales order with ID {id} not found");
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting sales order with ID {SalesOrderId}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<SalesOrderDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<SalesOrderDto>>> GetAllSalesOrders()
        {
            try
            {
                var result = await _queryHandler.GetAllSalesOrders(CancellationToken.None);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all sales orders");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        [HttpGet("customer/{customerId}")]
        [ProducesResponseType(typeof(IEnumerable<SalesOrderDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<SalesOrderDto>>> GetSalesOrdersByCustomer(Guid customerId)
        {
            try
            {
                var result = await _queryHandler.GetSalesOrdersByCustomer(customerId, CancellationToken.None);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting sales orders for customer {CustomerId}", customerId);
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        [HttpGet("status/{status}")]
        [ProducesResponseType(typeof(IEnumerable<SalesOrderDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<SalesOrderDto>>> GetSalesOrdersByStatus(string status)
        {
            try
            {
                var result = await _queryHandler.GetSalesOrdersByStatus(status, CancellationToken.None);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting sales orders with status {Status}", status);
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        [HttpGet("statuses")]
        [ProducesResponseType(typeof(IEnumerable<string>), StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<string>> GetOrderStatuses()
        {
            var statuses = new[]
            {
                "Draft",
                "Pending",
                "Confirmed",
                "Processing",
                "Shipped",
                "Delivered",
                "Completed",
                "Cancelled"
            };

            return Ok(statuses);
        }
    }
}
