﻿using ERP.Application.Core.Auth.Commands.Users;
using ERP.Domain.DTOs.Auth;
using ERP.Domain.Repositories;
using ERP.Domain.Entities.Auth;

namespace ERP.Application.Core.Auth.Commands.Handlers
{
    public class UserCommandHandler : IUserCommandHandler
    {
        private readonly CreateUser _createUser;
        private readonly UpdateUser _updateUser;
        private readonly DeleteUser _deleteUser;
        private readonly ChangePassword _changePassword;
        private readonly UpdateUserAdditionalData _updateUserAdditionalData;
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly IRepositoryBase<Role> _roleRepository;

        public UserCommandHandler(
            CreateUser createUser, 
            UpdateUser updateUser, 
            DeleteUser deleteUser, 
            ChangePassword changePassword,
            UpdateUserAdditionalData updateUserAdditionalData,
            IUserRoleRepository userRoleRepository,
            IRepositoryBase<Role> roleRepository)
        {
            _createUser = createUser;
            _updateUser = updateUser;
            _deleteUser = deleteUser;
            _changePassword = changePassword;
            _updateUserAdditionalData = updateUserAdditionalData;
            _userRoleRepository = userRoleRepository;
            _roleRepository = roleRepository;
        }

        public async Task<UserDto> CreateUser(CreateUserDto command, CancellationToken cancellationToken)
        {
            return await _createUser.HandleAsync(command, cancellationToken);
        }

        public async Task<UserDto> UpdateUser(Guid id, UpdateUserDto command, CancellationToken cancellationToken)
        {
            return await _updateUser.HandleAsync(id, command, cancellationToken);
        }

        public async Task<bool> DeleteUser(Guid id, CancellationToken cancellationToken)
        {
            return await _deleteUser.HandleAsync(id, cancellationToken);
        }

        public async Task<bool> ChangePassword(Guid userId, ChangePasswordDto command, CancellationToken cancellationToken)
        {
            return await _changePassword.HandleAsync(userId, command, cancellationToken);
        }

        public async Task<UserAdditionalValueResponseDto> SetUserAdditionalValue(Guid userId, UserAdditionalDataOperationDto operationDto, CancellationToken cancellationToken)
        {
            return await _updateUserAdditionalData.SetAdditionalValue(userId, operationDto, cancellationToken);
        }

        public async Task<UserAdditionalValueResponseDto> GetUserAdditionalValue(Guid userId, string key, CancellationToken cancellationToken)
        {
            return await _updateUserAdditionalData.GetAdditionalValue(userId, key, cancellationToken);
        }

        public async Task<bool> RemoveUserAdditionalValue(Guid userId, string key, CancellationToken cancellationToken)
        {
            return await _updateUserAdditionalData.RemoveAdditionalValue(userId, key, cancellationToken);
        }

        public async Task<Dictionary<string, object>> UpdateUserAdditionalData(Guid userId, UpdateUserAdditionalDataDto updateDto, CancellationToken cancellationToken)
        {
            return await _updateUserAdditionalData.UpdateAllAdditionalData(userId, updateDto, cancellationToken);
        }

        /// <summary>
        /// Asignar múltiples roles a un usuario
        /// </summary>
        public async Task<MultipleRoleAssignmentResult> AssignMultipleRolesToUser(AssignMultipleRolesToUser command, CancellationToken cancellationToken)
        {
            var result = new MultipleRoleAssignmentResult();

            foreach (var roleId in command.RoleIds)
            {
                try
                {
                    // Verificar si el rol existe y está activo
                    var role = await _roleRepository.Find(r => r.Id == roleId, cancellationToken);
                    if (role == null || !role.Status)
                    {
                        result.FailedRoles.Add($"Role {roleId} not found or inactive");
                        continue;
                    }

                    // Verificar si ya está asignado
                    var isAlreadyAssigned = await _userRoleRepository.UserHasRoleAsync(command.UserId, roleId, cancellationToken);
                    if (isAlreadyAssigned)
                    {
                        result.ExistingRoles.Add(new UserRoleDto
                        {
                            Id = role.Id,
                            Name = role.Name,
                            Description = role.Description,
                            Status = role.Status
                        });
                        continue;
                    }

                    // Asignar el rol
                    var assigned = await _userRoleRepository.AssignRoleToUserAsync(command.UserId, roleId, cancellationToken);
                    if (assigned)
                    {
                        result.AssignedRoles.Add(new UserRoleDto
                        {
                            Id = role.Id,
                            Name = role.Name,
                            Description = role.Description,
                            Status = role.Status
                        });
                    }
                    else
                    {
                        result.FailedRoles.Add($"Failed to assign role {roleId}");
                    }
                }
                catch (Exception ex)
                {
                    result.FailedRoles.Add($"Error assigning role {roleId}: {ex.Message}");
                }
            }

            return result;
        }

        /// <summary>
        /// Remover múltiples roles de un usuario
        /// </summary>
        public async Task<MultipleRoleRemovalResult> RemoveMultipleRolesFromUser(RemoveMultipleRolesFromUser command, CancellationToken cancellationToken)
        {
            var result = new MultipleRoleRemovalResult();

            foreach (var roleId in command.RoleIds)
            {
                try
                {
                    // Verificar si el usuario tiene el rol asignado
                    var hasRole = await _userRoleRepository.UserHasRoleAsync(command.UserId, roleId, cancellationToken);
                    if (!hasRole)
                    {
                        result.NotAssignedRoles.Add(roleId);
                        continue;
                    }

                    // Remover el rol
                    var removed = await _userRoleRepository.RemoveRoleFromUserAsync(command.UserId, roleId, cancellationToken);
                    if (removed)
                    {
                        result.RemovedRoles.Add(roleId);
                    }
                    else
                    {
                        result.FailedRoles.Add($"Failed to remove role {roleId}");
                    }
                }
                catch (Exception ex)
                {
                    result.FailedRoles.Add($"Error removing role {roleId}: {ex.Message}");
                }
            }

            return result;
        }

        /// <summary>
        /// Obtener todos los roles de un usuario
        /// </summary>
        public async Task<List<UserRoleDto>> GetUserRoles(Guid userId, CancellationToken cancellationToken)
        {
            var roles = new List<UserRoleDto>();
            var userRoles = await _userRoleRepository.GetUserRolesAsync(userId, cancellationToken);

            foreach (var userRole in userRoles)
            {
                var role = await _roleRepository.Find(r => r.Id == userRole.RoleId, cancellationToken);
                if (role != null && role.Status)
                {
                    roles.Add(new UserRoleDto
                    {
                        Id = role.Id,
                        Name = role.Name,
                        Description = role.Description,
                        Status = role.Status
                    });
                }
            }

            return roles.OrderBy(r => r.Name).ToList();
        }
    }
}
