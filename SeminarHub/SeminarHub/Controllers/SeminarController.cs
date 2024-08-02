using Microsoft.AspNetCore.Mvc;

namespace SeminarHub.Controllers
{
	public class SeminarController : BaseController
	{
		public IActionResult All()
		{
			return View();
		}
	}
}
