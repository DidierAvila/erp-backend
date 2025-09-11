﻿using AutoMapper;
using ERP.Domain.DTOs.Auth;
using ERP.Domain.Entities.Auth;
using ERP.Domain.Repositories;

namespace ERP.Application.Core.Auth.Commands.Users
{
    public class UpdateUser
    {
        private readonly IRepositoryBase<User> _userRepository;
        private readonly IRepositoryBase<ERP.Domain.Entities.Auth.UserTypes> _userTypeRepository;
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly IMapper _mapper;

        public UpdateUser(IRepositoryBase<User> userRepository, IRepositoryBase<ERP.Domain.Entities.Auth.UserTypes> userTypeRepository, IUserRoleRepository userRoleRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _userTypeRepository = userTypeRepository;
            _userRoleRepository = userRoleRepository;
            _mapper = mapper;
        }

        public async Task<UserDto> HandleAsync(Guid id, UpdateUserDto updateUserDto, CancellationToken cancellationToken)
        {
            // Find existing user
            var user = await _userRepository.Find(x => x.Id == id, cancellationToken);
            if (user == null)
                throw new KeyNotFoundException("User not found");

            // Map DTO properties to existing entity using AutoMapper
            _mapper.Map(updateUserDto, user);
            
            // Ensure UpdatedAt is set
            user.UpdatedAt = DateTime.UtcNow;

            // Update in repository
            await _userRepository.Update(user, cancellationToken);

            // Gestionar roles si se proporcionaron
            if (updateUserDto.RoleIds != null)
            {
                await UpdateUserRoles(user.Id, updateUserDto.RoleIds, cancellationToken);
            }

            // Obtener el UserType para incluir el nombre
            var userType = await _userTypeRepository.Find(x => x.Id == user.UserTypeId, cancellationToken);

            // Map Entity to DTO using AutoMapper
            var userDto = _mapper.Map<UserDto>(user);
            userDto.UserTypeName = userType?.Name;

            // Cargar roles del usuario
            userDto.Roles = await LoadUserRoles(user.Id, cancellationToken);

            return userDto;
        }

        private async Task<List<UserRoleDto>> LoadUserRoles(Guid userId, CancellationToken cancellationToken)
        {
            var userRoles = await _userRoleRepository.GetUserRolesWithDetailsAsync(userId, cancellationToken);
            return _mapper.Map<List<UserRoleDto>>(userRoles);
        }

        private async Task UpdateUserRoles(Guid userId, List<Guid> newRoleIds, CancellationToken cancellationToken)
        {
            // Remover todos los roles actuales del usuario
            await _userRoleRepository.RemoveAllUserRolesAsync(userId, cancellationToken);

            // Asignar los nuevos roles
            if (newRoleIds != null && newRoleIds.Any())
            {
                foreach (var roleId in newRoleIds)
                {
                    await _userRoleRepository.AssignRoleToUserAsync(userId, roleId, cancellationToken);
                }
            }
        }
    }
}
