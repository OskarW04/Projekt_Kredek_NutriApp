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


        public Guid? UserId
        {
            get
            {
                var value = UserClaimsPrincipal?.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                if (value == null) return null;
                if (!Guid.TryParse(value, out var userId))
                {
                    return null;
                }

                return userId;
            }
        }

        public string? Email
        {
            get
            {
                var value = UserClaimsPrincipal?.FindFirst(c => c.Type == ClaimTypes.Email)?.Value;
                return value;
            }
        }
    }
}