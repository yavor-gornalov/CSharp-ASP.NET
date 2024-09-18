﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
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

        return View(new AdServiceModel
        {
            Categories = categories
        });
    }

    [HttpPost]
    public async Task<IActionResult> Add(AdServiceModel model)
    {
        var ownerId = UserId;

        if (!IsUserLoggedIn || ownerId == null)
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

        await adService.AddAsync(model, ownerId);

        return RedirectToAction(nameof(All));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var ad = await adService.GetByIdAsync(id);

        if (ad == null)
        {
            return NotFound();
        }

        if (!IsUserLoggedIn || ad.OwnerId != UserId)
        {
            return Unauthorized();
        }

        var model = new AdServiceModel
        {
            Name = ad.Name,
            Description = ad.Description,
            Price = ad.Price.ToString(),
            ImageUrl = ad.ImageUrl,
            CategoryId = ad.CategoryId,
            Categories = await categoryService.AllAsync()
        };

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, AdServiceModel model)
    {
        var ad = await adService.GetByIdAsync(id);

        if (ad == null)
        {
            return NotFound();
        }

        if (!IsUserLoggedIn || ad.OwnerId != UserId)
        {
            return Unauthorized();
        }

        if (await categoryService.ExistsAsync(model.CategoryId) == false)
        {
            ModelState.AddModelError(nameof(model.CategoryId), "Category does not exist.");
        }

        if (!ModelState.IsValid)
        {
            model.Categories = await categoryService.AllAsync();
            return View(model);
        }

        await adService.EditAsync(id, model);

        return RedirectToAction(nameof(All));
    }
}
