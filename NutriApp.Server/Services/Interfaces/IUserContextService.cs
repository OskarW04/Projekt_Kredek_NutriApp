using System.Security.Claims;

namespace NutriApp.Server.Services.Interfaces
{
    public interface IUserContextService
    {
        ClaimsPrincipal? UserClaimsPrincipal { get; }
        string? UserId { get; }
        string? Email { get; }
    }
}