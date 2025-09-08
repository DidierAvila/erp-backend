﻿using AutoMapper;
using ERP.Domain.Entities;
using ERP.Domain.Repositories;

namespace ERP.Application.Core.Auth.Commands.Roles
{
    public class DeleteRole
    {
        private readonly IRepositoryBase<Role> _roleRepository;

        public DeleteRole(IRepositoryBase<Role> roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<bool> HandleAsync(Guid id, CancellationToken cancellationToken)
        {
            // Find existing role
            var role = await _roleRepository.Find(x => x.Id == id, cancellationToken);
            if (role == null)
                throw new KeyNotFoundException("Role not found");

            // Delete role from repository
            await _roleRepository.Delete(role, cancellationToken);

            return true;
        }
    }
}
