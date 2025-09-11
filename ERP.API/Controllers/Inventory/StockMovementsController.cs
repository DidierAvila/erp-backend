using ERP.Application.Core.Inventory.StockMovements.Commands.CreateStockMovement;
using ERP.Application.Core.Inventory.StockMovements.Commands.DeleteStockMovement;
using ERP.Application.Core.Inventory.StockMovements.Commands.UpdateStockMovement;
using ERP.Application.Core.Inventory.StockMovements.Queries.GetAllStockMovements;
using ERP.Application.Core.Inventory.StockMovements.Queries.GetStockMovementById;
using ERP.Domain.DTOs.Common;
using ERP.Domain.DTOs.Inventory;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ERP.API.Controllers.Inventory
{
    /// <summary>
    /// Controlador para gestionar los movimientos de inventario.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StockMovementsController : ControllerBase
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="StockMovementsController"/>.
        /// </summary>
        /// <param name="mediator">Instancia de <see cref="IMediator"/> para manejar las solicitudes.</param>
        public StockMovementsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Obtiene todos los movimientos de inventario.
        /// </summary>
        /// <returns>Una lista de movimientos de inventario.</returns>
        [HttpGet]
        public async Task<ActionResult<ResponseDto<List<StockMovementDto>>>> GetAllStockMovements()
        {
            try
            {
                var query = new GetAllStockMovementsQuery();
                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, new ResponseDto<List<StockMovementDto>>
                {
                    IsSuccess = false,
                    Message = "Error interno del servidor",
                    Data = null
                });
            }
        }

        /// <summary>
        /// Obtiene un movimiento de inventario por ID.
        /// </summary>
        /// <param name="id">El ID del movimiento de inventario.</param>
        /// <returns>El movimiento de inventario correspondiente al ID proporcionado.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseDto<StockMovementDto>>> GetStockMovementById(int id)
        {
            try
            {
                var query = new GetStockMovementByIdQuery { Id = id };
                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, new ResponseDto<StockMovementDto>
                {
                    IsSuccess = false,
                    Message = "Error interno del servidor",
                    Data = null
                });
            }
        }

        /// <summary>
        /// Crea un nuevo movimiento de inventario.
        /// </summary>
        /// <param name="createStockMovementDto">Datos para crear el movimiento de inventario.</param>
        /// <returns>El movimiento de inventario creado.</returns>
        [HttpPost]
        public async Task<ActionResult<ResponseDto<StockMovementDto>>> CreateStockMovement([FromBody] CreateStockMovementDto createStockMovementDto)
        {
            try
            {
                var command = new CreateStockMovementCommand { CreateStockMovementDto = createStockMovementDto };
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, new ResponseDto<StockMovementDto>
                {
                    IsSuccess = false,
                    Message = "Error interno del servidor",
                    Data = null
                });
            }
        }

        /// <summary>
        /// Actualiza un movimiento de inventario existente.
        /// </summary>
        /// <param name="id">El ID del movimiento de inventario a actualizar.</param>
        /// <param name="updateStockMovementDto">Datos para actualizar el movimiento de inventario.</param>
        /// <returns>El movimiento de inventario actualizado.</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseDto<StockMovementDto>>> UpdateStockMovement(int id, [FromBody] UpdateStockMovementDto updateStockMovementDto)
        {
            try
            {
                var command = new UpdateStockMovementCommand { Id = id, UpdateStockMovementDto = updateStockMovementDto };
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, new ResponseDto<StockMovementDto>
                {
                    IsSuccess = false,
                    Message = "Error interno del servidor",
                    Data = null
                });
            }
        }

        /// <summary>
        /// Elimina un movimiento de inventario por ID.
        /// </summary>
        /// <param name="id">El ID del movimiento de inventario a eliminar.</param>
        /// <returns>Un valor booleano indicando si la operación fue exitosa.</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseDto<bool>>> DeleteStockMovement(int id)
        {
            try
            {
                var command = new DeleteStockMovementCommand { Id = id };
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, new ResponseDto<bool>
                {
                    IsSuccess = false,
                    Message = "Error interno del servidor",
                    Data = false
                });
            }
        }
    }
}
