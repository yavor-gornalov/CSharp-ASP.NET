using Microsoft.AspNetCore.Mvc;
using SoftUniBazar.Core.Contracts;

namespace SoftUniBazar.Controllers;

public class AdController : BaseController
{
    private readonly IAdService adService;

    public AdController(IAdService adService)
    {
        this.adService = adService;
    }

    public async Task<IActionResult> All()
    {
        var model =  await adService.AllAsync();

        return View(model);
    }
}
