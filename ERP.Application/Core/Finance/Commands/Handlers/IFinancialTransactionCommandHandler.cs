using ERP.Domain.DTOs.Finance;

namespace ERP.Application.Core.Finance.Commands.Handlers
{
    public interface IFinancialTransactionCommandHandler
    {
        Task<FinancialTransactionDto> CreateFinancialTransaction(CreateFinancialTransactionDto command, CancellationToken cancellationToken);
        Task<FinancialTransactionDto> UpdateFinancialTransaction(int id, UpdateFinancialTransactionDto command, CancellationToken cancellationToken);
        Task<bool> DeleteFinancialTransaction(int id, CancellationToken cancellationToken);
    }
}
