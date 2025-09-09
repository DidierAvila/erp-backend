using ERP.Application.Core.Auth.Commands.System;
using Microsoft.AspNetCore.Mvc;

namespace ERP.API.Controllers.Auth
{
    [ApiController]
    [Route("api/[controller]")]
    public class SystemController : ControllerBase
    {
        private readonly InitializeSystemData _initializeSystemData;

        public SystemController(InitializeSystemData initializeSystemData)
        {
            _initializeSystemData = initializeSystemData;
        }

        /// <summary>
        /// Inicializa los datos básicos del sistema (UserTypes, Roles, Permisos)
        /// </summary>
        [HttpPost("initialize")]
        public async Task<ActionResult<bool>> InitializeSystem([FromBody] InitializeSystemRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _initializeSystemData.HandleAsync(
                    request.AdminEmail, 
                    request.AdminPassword, 
                    cancellationToken);
                
                return Ok(new { Success = result, Message = "Sistema inicializado correctamente" });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { Success = false, Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Success = false, Message = "Error interno del servidor", Details = ex.Message });
            }
        }
    }

    public class InitializeSystemRequest
    {
        public string? AdminEmail { get; set; }
        public string? AdminPassword { get; set; }
    }
}
