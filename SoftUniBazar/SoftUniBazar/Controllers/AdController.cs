using Microsoft.AspNetCore.Mvc;
using SoftUniBazar.Core.Contracts;
using SoftUniBazar.Core.Models;

namespace SoftUniBazar.Controllers;

public class AdController : BaseController
{
    private readonly IAdService adService;
    private readonly ICategoryService categoryService;

    public AdController(
        IAdService adService,
        ICategoryService categoryService)
    {
        this.adService = adService;
        this.categoryService = categoryService;
    }

    public async Task<IActionResult> All()
    {
        var model = await adService.AllAsync();

        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> Add()
    {
        var categories = await categoryService.AllAsync();

        return View(new AdAddViewModel
        {
            Categories = categories
        });
    }

    [HttpPost]
    public async Task<IActionResult> Add (AdAddViewModel model)
    {
        if (!IsUserLoggedIn)
        {
            return Unauthorized();
        }

        if (await categoryService.ExistsAsync(model.CategoryId) == false)
        {
            return BadRequest();
        }

        if (!ModelState.IsValid)
        {
            model.Categories = await categoryService.AllAsync();
            return View(model);
        }

        var ownerId = UserId;
        await adService.AddAsync(model, ownerId);

        return RedirectToAction(nameof(All));
    }
}
