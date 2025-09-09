using ERP.Domain.DTOs.Auth;
using ERP.Domain.Entities.Auth;
using ERP.Domain.Repositories;

namespace ERP.Application.Core.Auth.Commands.Users
{
    public class UpdateUserAdditionalData
    {
        private readonly IRepositoryBase<User> _userRepository;

        public UpdateUserAdditionalData(IRepositoryBase<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserAdditionalValueResponseDto> SetAdditionalValue(
            Guid userId, 
            UserAdditionalDataOperationDto operationDto, 
            CancellationToken cancellationToken)
        {
            var user = await _userRepository.Find(x => x.Id == userId, cancellationToken);
            if (user == null)
                throw new KeyNotFoundException("User not found");

            user.SetAdditionalValue(operationDto.Key, operationDto.Value);
            await _userRepository.Update(user, cancellationToken);

            return new UserAdditionalValueResponseDto
            {
                Key = operationDto.Key,
                Value = operationDto.Value,
                Exists = true
            };
        }

        public async Task<UserAdditionalValueResponseDto> GetAdditionalValue(
            Guid userId, 
            string key, 
            CancellationToken cancellationToken)
        {
            var user = await _userRepository.Find(x => x.Id == userId, cancellationToken);
            if (user == null)
                throw new KeyNotFoundException("User not found");

            var exists = user.HasAdditionalValue(key);
            var value = exists ? user.GetAdditionalValue<object>(key) : null;

            return new UserAdditionalValueResponseDto
            {
                Key = key,
                Value = value,
                Exists = exists
            };
        }

        public async Task<bool> RemoveAdditionalValue(
            Guid userId, 
            string key, 
            CancellationToken cancellationToken)
        {
            var user = await _userRepository.Find(x => x.Id == userId, cancellationToken);
            if (user == null)
                throw new KeyNotFoundException("User not found");

            var removed = user.RemoveAdditionalValue(key);
            if (removed)
            {
                await _userRepository.Update(user, cancellationToken);
            }

            return removed;
        }

        public async Task<Dictionary<string, object>> UpdateAllAdditionalData(
            Guid userId, 
            UpdateUserAdditionalDataDto updateDto, 
            CancellationToken cancellationToken)
        {
            var user = await _userRepository.Find(x => x.Id == userId, cancellationToken);
            if (user == null)
                throw new KeyNotFoundException("User not found");

            user.AdditionalData = updateDto.AdditionalData;
            await _userRepository.Update(user, cancellationToken);

            return user.AdditionalData;
        }
    }
}
