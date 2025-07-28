using System.Security.Claims;
using SAS.EventsService.Application.Contracts.Providers;
using Microsoft.AspNetCore.Http;

namespace SAS.EventsService.Infrastructure.Services.Providers
{
    public class CurrentUserProvider : ICurrentUserProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserProvider(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Guid UserId
        {
            get
            {
                var user = _httpContextAccessor.HttpContext?.User;
                if (user == null)
                    return Guid.Empty;

                // Assuming UserId is stored as ClaimTypes.NameIdentifier or "sub"
                var userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier) ?? user.FindFirst("sub");

                if (userIdClaim != null && Guid.TryParse(userIdClaim.Value, out var userId))
                {
                    return userId;
                }
                return Guid.Empty;
            }
        }

        public string Email
        {
            get
            {
                var user = _httpContextAccessor.HttpContext?.User;
                if (user == null)
                    return string.Empty;

                // Email claim usually ClaimTypes.Email
                var emailClaim = user.FindFirst(ClaimTypes.Email);
                return emailClaim?.Value ?? string.Empty;
            }
        }

        public IEnumerable<string> Roles
        {
            get
            {
                var user = _httpContextAccessor.HttpContext?.User;
                if (user == null)
                    return Enumerable.Empty<string>();

                // Roles are usually under ClaimTypes.Role
                var roles = user.FindAll(ClaimTypes.Role).Select(c => c.Value);
                return roles;
            }
        }
    }
}
