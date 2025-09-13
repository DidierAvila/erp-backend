using ERP.Application.Core.Finance.Commands.Handlers;
using ERP.Application.Core.Finance.Queries.Handlers;
using ERP.Domain.DTOs.Finance;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ERP.API.Attributes;

namespace ERP.API.Controllers.Finance
{
    /// <summary>
    /// Controlador para gestionar transacciones financieras.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FinancialTransactionsController : ControllerBase
    {
        private readonly IFinancialTransactionCommandHandler _commandHandler;
        private readonly IFinancialTransactionQueryHandler _queryHandler;

        /// <summary>
        /// Constructor del controlador FinancialTransactionsController
        /// </summary>
        /// <param name="commandHandler"></param>
        /// <param name="queryHandler"></param>
        public FinancialTransactionsController(
            IFinancialTransactionCommandHandler commandHandler,
            IFinancialTransactionQueryHandler queryHandler)
        {
            _commandHandler = commandHandler;
            _queryHandler = queryHandler;
        }

        /// <summary>
        /// Obtiene todas las transacciones financieras
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet]
        [RequirePermission("financial_transactions.read")]
        public async Task<ActionResult<IEnumerable<FinancialTransactionDto>>> GetAllTransactions(CancellationToken cancellationToken)
        {
            try
            {
                var transactions = await _queryHandler.GetAllFinancialTransactions(cancellationToken);
                return Ok(transactions);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Obtiene una transacción financiera por ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [RequirePermission("financial_transactions.read")]
        public async Task<ActionResult<FinancialTransactionDto>> GetTransactionById(int id, CancellationToken cancellationToken)
        {
            try
            {
                var transaction = await _queryHandler.GetFinancialTransactionById(id, cancellationToken);
                if (transaction == null)
                    return NotFound("Transaction not found");

                return Ok(transaction);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        /// <summary>
        /// Obtiene una transacción financiera por tipo
        /// </summary>
        /// <param name="type"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("by-type/{type}")]
        [RequirePermission("financial_transactions.read")]
        public async Task<ActionResult<IEnumerable<FinancialTransactionDto>>> GetTransactionsByType(string type, CancellationToken cancellationToken)
        {
            try
            {
                var transactions = await _queryHandler.GetTransactionsByType(type, cancellationToken);
                return Ok(transactions);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Crea una nueva transacción financiera
        /// </summary>
        /// <param name="createDto"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        [RequirePermission("financial_transactions.create")]
        public async Task<ActionResult<FinancialTransactionDto>> CreateTransaction([FromBody] CreateFinancialTransactionDto createDto, CancellationToken cancellationToken)
        {
            try
            {
                var createdTransaction = await _commandHandler.CreateFinancialTransaction(createDto, cancellationToken);
                return CreatedAtAction(nameof(GetTransactionById), new { id = createdTransaction.Id }, createdTransaction);
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
        /// Actualiza una transacci�n financiera existente
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateDto"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [RequirePermission("financial_transactions.update")]
        public async Task<ActionResult<FinancialTransactionDto>> UpdateTransaction(int id, [FromBody] UpdateFinancialTransactionDto updateDto, CancellationToken cancellationToken)
        {
            try
            {
                var updatedTransaction = await _commandHandler.UpdateFinancialTransaction(id, updateDto, cancellationToken);
                return Ok(updatedTransaction);
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
        /// Elimina una transacci�n financiera por ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [RequirePermission("financial_transactions.delete")]
        public async Task<ActionResult> DeleteTransaction(int id, CancellationToken cancellationToken)
        {
            try
            {
                await _commandHandler.DeleteFinancialTransaction(id, cancellationToken);
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
