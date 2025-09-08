using ERP.Application.Core.Finance.Commands.Handlers;
using ERP.Application.Core.Finance.Queries.Handlers;
using ERP.Domain.DTOs.Finance;
using Microsoft.AspNetCore.Mvc;

namespace ERP.API.Controllers.Finance
{
    [Route("api/[controller]")]
    [ApiController]
    public class FinancialTransactionsController : ControllerBase
    {
        private readonly IFinancialTransactionCommandHandler _commandHandler;
        private readonly IFinancialTransactionQueryHandler _queryHandler;

        public FinancialTransactionsController(
            IFinancialTransactionCommandHandler commandHandler,
            IFinancialTransactionQueryHandler queryHandler)
        {
            _commandHandler = commandHandler;
            _queryHandler = queryHandler;
        }

        // GET: api/FinancialTransactions
        [HttpGet]
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

        // GET: api/FinancialTransactions/{id}
        [HttpGet("{id}")]
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

        // GET: api/FinancialTransactions/by-type/{type}
        [HttpGet("by-type/{type}")]
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

        // GET: api/FinancialTransactions/summary?startDate=2024-01-01&endDate=2024-12-31
        [HttpGet("summary")]
        public async Task<ActionResult<FinancialSummaryDto>> GetFinancialSummary(
            [FromQuery] DateOnly startDate, 
            [FromQuery] DateOnly endDate, 
            CancellationToken cancellationToken)
        {
            try
            {
                var summary = await _queryHandler.GetFinancialSummary(startDate, endDate, cancellationToken);
                return Ok(summary);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: api/FinancialTransactions
        [HttpPost]
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

        // PUT: api/FinancialTransactions/{id}
        [HttpPut("{id}")]
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

        // DELETE: api/FinancialTransactions/{id}
        [HttpDelete("{id}")]
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
