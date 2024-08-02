using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SeminarHub.Controllers
{
	[Authorize]
	public class BaseController : Controller
	{
	}
}
