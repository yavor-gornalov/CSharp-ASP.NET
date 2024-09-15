using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace SoftUniBazar.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
        public bool IsUserLoggedIn
            => this.User?.Identity?.IsAuthenticated ?? false;

        public string? UserId
            => this.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? null;
    }
}
