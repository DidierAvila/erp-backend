using AutoMapper;
using ERP.Domain.DTOs.Auth;
using ERP.Domain.Entities.Auth;
using ERP.Domain.Repositories;
using BC = BCrypt.Net.BCrypt;

namespace ERP.Application.Core.Auth.Commands.Users
{
    public class CreateUser
    {
        private readonly IRepositoryBase<User> _userRepository;
        private readonly IRepositoryBase<ERP.Domain.Entities.Auth.UserTypes> _userTypeRepository;
        private readonly IRepositoryBase<Role> _roleRepository;
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly IMapper _mapper;

        public CreateUser(IRepositoryBase<User> userRepository, IRepositoryBase<ERP.Domain.Entities.Auth.UserTypes> userTypeRepository, IRepositoryBase<Role> roleRepository, IUserRoleRepository userRoleRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _userTypeRepository = userTypeRepository;
            _roleRepository = roleRepository;
            _userRoleRepository = userRoleRepository;
            _mapper = mapper;
        }

        public async Task<UserDto> HandleAsync(CreateUserDto createUserDto, CancellationToken cancellationToken)
        {
            // Validations
            if (string.IsNullOrWhiteSpace(createUserDto.Email))
                throw new ArgumentException("Email is required");

            if (createUserDto.UserTypeId == Guid.Empty)
                throw new ArgumentException("UserTypeId is required");

            // Check if user already exists
            var existingUser = await _userRepository.Find(x => x.Email == createUserDto.Email, cancellationToken);
            if (existingUser != null)
                throw new InvalidOperationException("User with this email already exists");

            // Map DTO to Entity using AutoMapper
            var user = _mapper.Map<User>(createUserDto);

            // Encrypt password before saving
            if (!string.IsNullOrEmpty(user.Password))
            {
                user.Password = BC.HashPassword(user.Password, 12);
            }

            // Create user in repository
            var createdUser = await _userRepository.Create(user, cancellationToken);

            // Asignar roles si se proporcionaron
            if (createUserDto.RoleIds != null && createUserDto.RoleIds.Any())
            {
                await AssignRolesToUser(createdUser.Id, createUserDto.RoleIds, cancellationToken);
            }

            // Obtener el UserType para incluir el nombre
            var userType = await _userTypeRepository.Find(x => x.Id == createdUser.UserTypeId, cancellationToken);

            // Map Entity to DTO using AutoMapper
            var userDto = _mapper.Map<UserDto>(createdUser);
            userDto.UserTypeName = userType?.Name;

            // Cargar roles asignados para incluir en la respuesta
            if (createUserDto.RoleIds != null && createUserDto.RoleIds.Any())
            {
                userDto.Roles = await LoadUserRoles(createdUser.Id, cancellationToken);
            }

            return userDto;
        }

        /// <summary>
        /// Asigna múltiples roles a un usuario recién creado
        /// </summary>
        private async Task AssignRolesToUser(Guid userId, List<Guid> roleIds, CancellationToken cancellationToken)
        {
            // Validar que todos los roles existen
            var validRoles = new List<Role>();
            foreach (var roleId in roleIds)
            {
                var role = await _roleRepository.Find(r => r.Id == roleId && r.Status, cancellationToken);
                if (role != null)
                {
                    validRoles.Add(role);
                }
            }

            // Asignar los roles válidos al usuario
            if (validRoles.Any())
            {
                foreach (var role in validRoles)
                {
                    await _userRoleRepository.AssignRoleToUserAsync(userId, role.Id, cancellationToken);
                }
            }
        }

        /// <summary>
        /// Carga los roles del usuario para incluir en la respuesta
        /// </summary>
        private async Task<List<UserRoleDto>> LoadUserRoles(Guid userId, CancellationToken cancellationToken)
        {
            var userRoles = await _userRoleRepository.GetUserRolesWithDetailsAsync(userId, cancellationToken);
            
            return userRoles
                .Where(ur => ur.Role != null && ur.Role.Status)
                .Select(ur => new UserRoleDto
                {
                    Id = ur.Role.Id,
                    Name = ur.Role.Name,
                    Description = ur.Role.Description,
                    Status = ur.Role.Status
                })
                .ToList();
        }
    }
}
