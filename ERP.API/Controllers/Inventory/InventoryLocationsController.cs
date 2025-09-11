using ERP.Application.Core.Inventory.InventoryLocations.Commands.CreateInventoryLocation;
using ERP.Application.Core.Inventory.InventoryLocations.Commands.DeleteInventoryLocation;
using ERP.Application.Core.Inventory.InventoryLocations.Commands.UpdateInventoryLocation;
using ERP.Application.Core.Inventory.InventoryLocations.Queries.GetAllInventoryLocations;
using ERP.Application.Core.Inventory.InventoryLocations.Queries.GetInventoryLocationById;
using ERP.Domain.DTOs.Common;
using ERP.Domain.DTOs.Inventory;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ERP.API.Controllers.Inventory
{
    /// <summary>
    /// Controlador para la gestión de ubicaciones de inventario
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [Produces("application/json")]
    [Tags("Inventory Locations")]
    public class InventoryLocationsController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        /// <summary>
        /// Obtiene todas las ubicaciones de inventario
        /// </summary>
        /// <returns>Lista de ubicaciones de inventario</returns>
        /// <response code="200">Ubicaciones obtenidas exitosamente</response>
        /// <response code="401">No autorizado</response>
        /// <response code="500">Error interno del servidor</response>
        [HttpGet]
        [ProducesResponseType(typeof(ResponseDto<IEnumerable<InventoryLocationDto>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ResponseDto<IEnumerable<InventoryLocationDto>>), (int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult<ResponseDto<List<InventoryLocationDto>>>> GetAllInventoryLocations()
        {
            try
            {
                var query = new GetAllInventoryLocationsQuery();
                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, new ResponseDto<List<InventoryLocationDto>>
                {
                    IsSuccess = false,
                    Message = "Error interno del servidor",
                    Data = null
                });
            }
        }

        /// <summary>
        /// Obtiene una ubicación de inventario por su ID
        /// </summary>
        /// <param name="id">ID de la ubicación de inventario</param>
        /// <returns>Ubicación de inventario encontrada</returns>
        /// <response code="200">Ubicación encontrada exitosamente</response>
        /// <response code="401">No autorizado</response>
        /// <response code="404">Ubicación no encontrada</response>
        /// <response code="500">Error interno del servidor</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ResponseDto<InventoryLocationDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ResponseDto<InventoryLocationDto>), (int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult<ResponseDto<InventoryLocationDto>>> GetInventoryLocationById(int id)
        {
            try
            {
                var query = new GetInventoryLocationByIdQuery { Id = id };
                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, new ResponseDto<InventoryLocationDto>
                {
                    IsSuccess = false,
                    Message = "Error interno del servidor",
                    Data = null
                });
            }
        }

        /// <summary>
        /// Crea una nueva ubicación de inventario
        /// </summary>
        /// <param name="createInventoryLocationDto">Datos para crear la ubicación</param>
        /// <returns>Ubicación de inventario creada</returns>
        /// <response code="200">Ubicación creada exitosamente</response>
        /// <response code="400">Datos de entrada inválidos</response>
        /// <response code="401">No autorizado</response>
        /// <response code="409">Ya existe una ubicación con el mismo nombre</response>
        /// <response code="500">Error interno del servidor</response>
        [HttpPost]
        [ProducesResponseType(typeof(ResponseDto<InventoryLocationDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ResponseDto<InventoryLocationDto>), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ResponseDto<InventoryLocationDto>), (int)HttpStatusCode.Conflict)]
        [ProducesResponseType(typeof(ResponseDto<InventoryLocationDto>), (int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult<ResponseDto<InventoryLocationDto>>> CreateInventoryLocation([FromBody] CreateInventoryLocationDto createInventoryLocationDto)
        {
            try
            {
                var command = new CreateInventoryLocationCommand { CreateInventoryLocationDto = createInventoryLocationDto };
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, new ResponseDto<InventoryLocationDto>
                {
                    IsSuccess = false,
                    Message = "Error interno del servidor",
                    Data = null
                });
            }
        }

        /// <summary>
        /// Actualiza una ubicación de inventario existente
        /// </summary>
        /// <param name="id">ID de la ubicación a actualizar</param>
        /// <param name="updateInventoryLocationDto">Datos para actualizar la ubicación</param>
        /// <returns>Ubicación de inventario actualizada</returns>
        /// <response code="200">Ubicación actualizada exitosamente</response>
        /// <response code="400">Datos de entrada inválidos</response>
        /// <response code="401">No autorizado</response>
        /// <response code="404">Ubicación no encontrada</response>
        /// <response code="409">Ya existe otra ubicación con el mismo nombre</response>
        /// <response code="500">Error interno del servidor</response>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ResponseDto<InventoryLocationDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ResponseDto<InventoryLocationDto>), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ResponseDto<InventoryLocationDto>), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ResponseDto<InventoryLocationDto>), (int)HttpStatusCode.Conflict)]
        [ProducesResponseType(typeof(ResponseDto<InventoryLocationDto>), (int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult<ResponseDto<InventoryLocationDto>>> UpdateInventoryLocation(int id, [FromBody] UpdateInventoryLocationDto updateInventoryLocationDto)
        {
            try
            {
                var command = new UpdateInventoryLocationCommand { Id = id, UpdateInventoryLocationDto = updateInventoryLocationDto };
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, new ResponseDto<InventoryLocationDto>
                {
                    IsSuccess = false,
                    Message = "Error interno del servidor",
                    Data = null
                });
            }
        }

        /// <summary>
        /// Elimina una ubicación de inventario
        /// </summary>
        /// <param name="id">ID de la ubicación a eliminar</param>
        /// <returns>Resultado de la operación de eliminación</returns>
        /// <response code="200">Ubicación eliminada exitosamente</response>
        /// <response code="400">ID inválido</response>
        /// <response code="401">No autorizado</response>
        /// <response code="404">Ubicación no encontrada</response>
        /// <response code="409">No se puede eliminar porque tiene movimientos de stock asociados</response>
        /// <response code="500">Error interno del servidor</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ResponseDto<bool>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ResponseDto<bool>), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ResponseDto<bool>), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ResponseDto<bool>), (int)HttpStatusCode.Conflict)]
        [ProducesResponseType(typeof(ResponseDto<bool>), (int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult<ResponseDto<bool>>> DeleteInventoryLocation(int id)
        {
            try
            {
                var command = new DeleteInventoryLocationCommand { Id = id };
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
