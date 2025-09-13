using ERP.Application.Core.Finance.Commands.Handlers;
using ERP.Application.Core.Finance.Queries.Handlers;
using ERP.Domain.DTOs.Finance;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ERP.API.Attributes;

namespace ERP.API.Controllers.Finance
{
    /// <summary>
    /// Controlador para gestionar cuentas financieras.
    /// </summary>
    [ApiController]
    [Route("api/finance/[controller]")]
    [Authorize]
    public class AccountsController : ControllerBase
    {
        private readonly ILogger<AccountsController> _logger;
        private readonly IAccountCommandHandler _accountCommandHandler;
        private readonly IAccountQueryHandler _accountQueryHandler;

        /// <summary>
        /// Constructor del controlador AccountsController
        /// </summary>
        /// <param name="accountCommandHandler"></param>
        /// <param name="accountQueryHandler"></param>
        /// <param name="logger"></param>
        public AccountsController(
            IAccountCommandHandler accountCommandHandler,
            IAccountQueryHandler accountQueryHandler,
            ILogger<AccountsController> logger)
        {
            _accountCommandHandler = accountCommandHandler;
            _accountQueryHandler = accountQueryHandler;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene todas las cuentas
        /// </summary>
        [HttpGet]
        [RequirePermission("accounts.read")]
        public async Task<ActionResult<IEnumerable<AccountDto>>> GetAll(CancellationToken cancellationToken)
        {
            try
            {
                var accounts = await _accountQueryHandler.GetAllAccounts(cancellationToken);
                return Ok(accounts);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all accounts");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Obtiene una cuenta por ID
        /// </summary>
        [HttpGet("{id}")]
        [RequirePermission("accounts.read")]
        public async Task<ActionResult<AccountDto>> GetById(int id, CancellationToken cancellationToken)
        {
            try
            {
                var account = await _accountQueryHandler.GetAccountById(id, cancellationToken);
                if (account == null)
                    return NotFound($"Account with ID {id} not found");

                return Ok(account);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving account with ID {AccountId}", id);
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Crea una nueva cuenta
        /// </summary>
        [HttpPost]
        [RequirePermission("accounts.create")]
        public async Task<ActionResult<AccountDto>> Create([FromBody] CreateAccountDto createDto, CancellationToken cancellationToken)
        {
            try
            {
                var account = await _accountCommandHandler.CreateAccount(createDto, cancellationToken);
                return CreatedAtAction(nameof(GetById), new { id = account.Id }, account);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating account");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Actualiza una cuenta existente
        /// </summary>
        [HttpPut("{id}")]
        [RequirePermission("accounts.update")]
        public async Task<ActionResult<AccountDto>> Update(int id, [FromBody] UpdateAccountDto updateDto, CancellationToken cancellationToken)
        {
            try
            {
                var account = await _accountCommandHandler.UpdateAccount(id, updateDto, cancellationToken);
                return Ok(account);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating account with ID {AccountId}", id);
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Elimina una cuenta
        /// </summary>
        [HttpDelete("{id}")]
        [RequirePermission("accounts.delete")]
        public async Task<ActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _accountCommandHandler.DeleteAccount(id, cancellationToken);
                if (result)
                    return NoContent();
                
                return BadRequest("Failed to delete account");
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting account with ID {AccountId}", id);
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Obtiene cuentas por tipo
        /// </summary>
        [HttpGet("by-type/{accountType}")]
        [RequirePermission("accounts.read")]
        public async Task<ActionResult<IEnumerable<AccountDto>>> GetByType(string accountType, CancellationToken cancellationToken)
        {
            try
            {
                var accounts = await _accountQueryHandler.GetAccountsByType(accountType, cancellationToken);
                return Ok(accounts);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving accounts by type {AccountType}", accountType);
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Obtiene solo las cuentas activas (resumen)
        /// </summary>
        [HttpGet("active")]
        [RequirePermission("accounts.read")]
        public async Task<ActionResult<IEnumerable<AccountSummaryDto>>> GetActive(CancellationToken cancellationToken)
        {
            try
            {
                var accounts = await _accountQueryHandler.GetActiveAccounts(cancellationToken);
                return Ok(accounts);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving active accounts");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
