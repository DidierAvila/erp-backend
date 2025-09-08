using ERP.Application.Core.Inventory.Commands.InventoryLocations;
using ERP.Domain.DTOs.Inventory;

namespace ERP.Application.Core.Inventory.Commands.Handlers
{
    public interface IInventoryLocationCommandHandler
    {
        Task<Result<InventoryLocationDto>> Handle(CreateInventoryLocation command);
        Task<Result<InventoryLocationDto>> Handle(UpdateInventoryLocation command);
        Task<Result<bool>> Handle(DeleteInventoryLocation command);
    }

    // Result helper class
    public class Result<T>
    {
        public bool IsSuccess { get; set; }
        public T? Value { get; set; }
        public string Error { get; set; } = string.Empty;

        public static Result<T> Success(T value) => new() { IsSuccess = true, Value = value };
        public static Result<T> Failure(string error) => new() { IsSuccess = false, Error = error };
    }
}
