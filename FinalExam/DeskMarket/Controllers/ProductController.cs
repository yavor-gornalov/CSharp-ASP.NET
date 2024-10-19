using DeskMarket.Models.Product;
using DeskMarket.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DeskMarket.Controllers;

[Authorize]
public class ProductController(
    IProductService productService,
    ICategoryService categoryService) : Controller
{
    [AllowAnonymous]
    public async Task<IActionResult> Index()
    {
        var model = await productService.GetAllProductsAsync(User.Id());

        return View(model);
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> Details(int id)
    {
        var model = await productService.GetProductDetailsAsync(id, User.Id());

        if (model == null)
        {
            return BadRequest();
        }

        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> Add()
    {
        var model = new ProductServiceModel
        {
            Categories = await categoryService.GetAllCategoriesAsync()
        };
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Add(ProductServiceModel model)
    {
        if (!ModelState.IsValid)
        {
            model.Categories = await categoryService.GetAllCategoriesAsync();
            return View(model);
        }

        await productService.AddProductAsync(model, User.Id());

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var model = await productService.GetProductByIdAsync(id);

        if (model == null)
        {
            return NotFound();
        }

        if (model.SellerId != User.Id())
        {
            return Unauthorized();
        }

        model.Categories = await categoryService.GetAllCategoriesAsync();

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(ProductServiceModel model, int id)
    {
        if (!ModelState.IsValid)
        {
            model.Categories = await categoryService.GetAllCategoriesAsync();
            return View(model);
        }

        var product = await productService.GetProductByIdAsync(id);

        if (product == null)
        {
            return NotFound();
        }

        if (product.SellerId != User.Id())
        {
            return Unauthorized();
        }

        await productService.EditProductAsync(model, id);

        return RedirectToAction(nameof(Details), new { id });
    }

    [HttpGet]
    public async Task<IActionResult> Cart()
    {
        var model = await productService.GetClientCartProductsAsync(User.Id());

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> AddToCart(int id)
    {
        if (!await productService.IsProductExistsAsync(id))
        {
            return NotFound();
        }

        if (await productService.IsProductSellerAsync(id, User.Id()) || await productService.IsProductBuyerAsync(id, User.Id()))
        {
            return BadRequest();
        }

        await productService.AddProductToCartAsync(id, User.Id());

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> RemoveFromCart(int id)
    {
        if (!await productService.IsProductExistsAsync(id))
        {
            return NotFound();
        }

        if (!await productService.IsProductBuyerAsync(id, User.Id()))
        {
            return BadRequest();
        }

        await productService.RemoveProductFromCartAsync(id, User.Id());

        return RedirectToAction(nameof(Cart));
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var model = await productService.GetProductForDeleteAsync(id);

        if (model == null)
        {
            return NotFound();
        }

        if (model.SellerId != User.Id())
        {
            return Unauthorized();
        }

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(ProductDeleteViewModel model)
    {
        if (model == null)
        {
            return NotFound();
        }

        if (model.SellerId != User.Id())
        {
            return Unauthorized();
        }

        await productService.DeleteProductAsync(model.Id);

        return RedirectToAction(nameof(Index));
    }
}
