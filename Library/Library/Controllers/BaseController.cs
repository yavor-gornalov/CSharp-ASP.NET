using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Library.Controllers;

[Authorize]
public class BaseController : Controller
{
	public string? GetUserId() =>
		User.FindFirstValue(ClaimTypes.NameIdentifier) ?? null;
}
