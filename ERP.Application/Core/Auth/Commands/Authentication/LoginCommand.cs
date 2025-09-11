using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ERP.Application.Utils;
using ERP.Domain.DTOs;
using ERP.Domain.Entities.Auth;
using ERP.Domain.Repositories;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using BC = BCrypt.Net.BCrypt;

namespace ERP.Application.Core.Auth.Commands.Authentication
{
    public class LoginCommand : ILoginCommand
    {
        private readonly IRepositoryBase<User> _userRepository;
        private readonly IRepositoryBase<Domain.Entities.Auth.UserTypes> _userTypeRepository;
        private readonly IRepositoryBase<Session> _sessionRepository;
        private readonly ILogger<LoginCommand> _logger;
        private readonly IConfiguration _configuration;

        public LoginCommand(IRepositoryBase<User> userRepository, IRepositoryBase<Session> sessionRepository, IConfiguration configuration, ILogger<LoginCommand> logger, IRepositoryBase<Domain.Entities.Auth.UserTypes> userTypeRepository) 
        {
            _userRepository = userRepository;
            _userTypeRepository = userTypeRepository;
            _sessionRepository = sessionRepository;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<LoginResponse?> Login(LoginRequest autorizacion, CancellationToken cancellationToken)
        {
            // Buscar usuario solo por email
            User? CurrentUser = await _userRepository.Find(x => x.Email == autorizacion.Email, cancellationToken);
            
            // Verificar si el usuario existe y la contraseña es correcta
            if (CurrentUser != null && !string.IsNullOrEmpty(CurrentUser.Password))
            {
                bool isPasswordValid = BC.Verify(autorizacion.Password, CurrentUser.Password);
                if (isPasswordValid)
                {
                    CurrentUser.UserType = await _userTypeRepository.Find(x => x.Id == CurrentUser.UserTypeId, cancellationToken);
                    _logger.LogInformation("Login: success");
                    string CurrentToken = await GetToken(CurrentUser, cancellationToken);
                    LoginResponse loginResponse = new()
                    { Token = CurrentToken };

                    return loginResponse;
                }
                else
                {
                    _logger.LogWarning("Login: invalid password for user {Email}", autorizacion.Email);
                }
            }
            else
            {
                _logger.LogWarning("Login: user not found {Email}", autorizacion.Email);
            }
            return null;
        }

        private async Task<string> GenerateTokenAsync(User user, CancellationToken cancellationToken)
        {
            string? key = _configuration.GetValue<string>("JwtSettings:key");
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            // Crear los claims
            var claims = new[]
            {
                new Claim(CustomClaimTypes.UserId, user.Id.ToString()),
                new Claim(CustomClaimTypes.UserName, user.Name),
                new Claim(CustomClaimTypes.UserEmail, user.Email),
                new Claim(CustomClaimTypes.UserTypeId, user.UserTypeId.ToString()),
                new Claim(CustomClaimTypes.UserTypeName, user.UserType!.Name),
            };

            // Crear el token
            DateTime ExperiredDate = DateTime.Now.AddMinutes(60);
            JwtSecurityToken tokenJwt = new(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: ExperiredDate,
                signingCredentials: credentials
            );

            string Newtoken = new JwtSecurityTokenHandler().WriteToken(tokenJwt);

            //Se almacena el nuevo token como session
            if (!string.IsNullOrEmpty(Newtoken))
            {
                Session session = new()
                {
                    Id = Guid.NewGuid(),
                    SessionToken = Newtoken,
                    UserId = user.Id,
                    Expires = ExperiredDate
                };

                await _sessionRepository.Create(session, cancellationToken);
            }
            return Newtoken;
        }

        private async Task<string> RefreshSessionAsync(Session session, User user, CancellationToken cancellationToken)
        {
            // Eliminar sesión anterior
            await _sessionRepository.Delete((int)session.Id.GetHashCode(), cancellationToken);

            string currentToken = await GenerateTokenAsync(user, cancellationToken);
            return currentToken;
        }

        private async Task<string> GetToken(User user, CancellationToken cancellationToken)
        {
            var CurrentSession = await _sessionRepository.Find(x => x.UserId == user.Id && x.Expires > DateTime.Now, cancellationToken);
            if (CurrentSession != null)
            {
                if (CurrentSession.Expires.CompareTo(DateTime.Now) < 0)
                {
                    _logger.LogInformation("GetToken: Expiration Session UserId:" + user.Id);
                    return await RefreshSessionAsync(CurrentSession, user, cancellationToken);
                }
                return CurrentSession.SessionToken!;
            }
            else
            {
                string currentToken = await GenerateTokenAsync(user, cancellationToken);
                if (!string.IsNullOrEmpty(currentToken))
                {
                    return currentToken;
                }
            }
            return string.Empty;
        }
    }
}
