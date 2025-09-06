using ERP.Domain.DTOs;
using Microsoft.AspNetCore.Identity.Data;

namespace ERP.Application.Core.Auth.Commands.Authentication
{
    public interface ILoginCommand
    {
        Task<LoginResponse?> Login(LoginRequest autorizacion, CancellationToken cancellationToken);
    }
}