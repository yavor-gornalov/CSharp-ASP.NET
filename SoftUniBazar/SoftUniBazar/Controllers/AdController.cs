using Microsoft.AspNetCore.Mvc;

namespace SoftUniBazar.Controllers
{
    public class AdController : BaseController
    {
        public IActionResult All()
        {
            return View();
        }
    }
}
