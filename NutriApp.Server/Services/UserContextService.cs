using NutriApp.Server.Services.Interfaces;
using System.Security.Claims;

namespace NutriApp.Server.Services
{
    public class UserContextService : IUserContextService
    {
        private readonly IHttpContextAccessor _httpContext;

        public UserContextService(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
        }

        public ClaimsPrincipal? UserClaimsPrincipal => _httpContext.HttpContext?.User;


        public string? UserId
        {
            get
            {
                return UserClaimsPrincipal?
                    .FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            }
        }

        public string? Email => UserClaimsPrincipal?.Identity?.Name;
    }
}