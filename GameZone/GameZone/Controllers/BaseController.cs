using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GameZone.Controllers
{
	[Authorize]
	public class BaseController : Controller
	{
		public string? GetCurrentUserId()
			=> User.FindFirstValue(ClaimTypes.NameIdentifier) ?? null;
	}
}
