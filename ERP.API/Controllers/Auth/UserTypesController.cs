using ERP.Application.Core.Auth.Commands.Handlers;
using ERP.Application.Core.Auth.Queries.Handlers;
using ERP.Domain.DTOs.Auth;
using ERP.Domain.DTOs.Common;
using Microsoft.AspNetCore.Mvc;

namespace ERP.API.Controllers.Auth
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserTypesController : ControllerBase
    {
        private readonly IUserTypeCommandHandler _commandHandler;
        private readonly IUserTypeQueryHandler _queryHandler;
        private readonly ILogger<UserTypesController> _logger;

        public UserTypesController(
            IUserTypeCommandHandler commandHandler, 
            IUserTypeQueryHandler queryHandler,
            ILogger<UserTypesController> logger)
        {
            _commandHandler = commandHandler;
            _queryHandler = queryHandler;
            _logger = logger;
        }

        /// <summary>
        /// Create a new UserType
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<UserTypeDto>> CreateUserType([FromBody] CreateUserTypeDto command, CancellationToken cancellationToken)
        {
            try
            {
                var userType = await _commandHandler.CreateUserType(command, cancellationToken);
                return CreatedAtAction(nameof(GetUserTypeById), new { id = userType.Id }, userType);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Update an existing UserType
        /// </summary>
        [HttpPut("{id}")]
        public async Task<ActionResult<UserTypeDto>> UpdateUserType(Guid id, [FromBody] UpdateUserTypeDto command, CancellationToken cancellationToken)
        {
            try
            {
                var userType = await _commandHandler.UpdateUserType(id, command, cancellationToken);
                return Ok(userType);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Delete a UserType
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteUserType(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _commandHandler.DeleteUserType(id, cancellationToken);
                return Ok(result);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Get UserType by ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<UserTypeDto>> GetUserTypeById(Guid id, CancellationToken cancellationToken)
        {
            var userType = await _queryHandler.GetUserTypeById(id, cancellationToken);
            if (userType == null)
                return NotFound("UserType not found");

            return Ok(userType);
        }

        /// <summary>
        /// Obtiene una lista paginada de tipos de usuario con filtros opcionales
        /// </summary>
        /// <param name="filter">Filtros de búsqueda y paginación</param>
        /// <param name="cancellationToken">Token de cancelación</param>
        /// <returns>Lista paginada de tipos de usuario</returns>
        /// <remarks>
        /// Campos disponibles para SortBy: name, description, status
        /// 
        /// Ejemplo de uso:
        /// GET /api/usertypes?page=1&amp;pageSize=10&amp;name=admin&amp;status=true&amp;sortBy=name
        /// </remarks>
        [HttpGet]
        public async Task<ActionResult<PaginationResponseDto<UserTypeListResponseDto>>> GetAllUserTypes(
            [FromQuery] UserTypeFilterDto filter,
            CancellationToken cancellationToken)
        {
            try
            {
                var userTypes = await _queryHandler.GetAllUserTypesFiltered(filter, cancellationToken);
                return Ok(userTypes);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving user types with filters");
                return StatusCode(500, "An error occurred while retrieving user types");
            }
        }

        /// <summary>
        /// Get all UserTypes (simple list without pagination)
        /// </summary>
        [HttpGet("simple")]
        public async Task<ActionResult<IEnumerable<UserTypeDto>>> GetAllUserTypesSimple(CancellationToken cancellationToken)
        {
            var userTypes = await _queryHandler.GetAllUserTypes(cancellationToken);
            return Ok(userTypes);
        }

        /// <summary>
        /// Get active UserTypes only
        /// </summary>
        [HttpGet("active")]
        public async Task<ActionResult<IEnumerable<UserTypeDto>>> GetActiveUserTypes(CancellationToken cancellationToken)
        {
            var userTypes = await _queryHandler.GetActiveUserTypes(cancellationToken);
            return Ok(userTypes);
        }

        /// <summary>
        /// Get UserTypes summary with user count
        /// </summary>
        [HttpGet("summary")]
        public async Task<ActionResult<IEnumerable<UserTypeSummaryDto>>> GetUserTypesSummary(CancellationToken cancellationToken)
        {
            var userTypes = await _queryHandler.GetUserTypesSummary(cancellationToken);
            return Ok(userTypes);
        }
    }
}
