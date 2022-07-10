using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SaveToDateApp.Models;
using SaveToDateApp.Services;

namespace SaveToDateApp.Controllers;
[Authorize]
public class PantryProductsController : Controller
{
    private readonly IPantryProductsService _service;

    public PantryProductsController(IPantryProductsService service)
    {
        _service = service;
    }
    
    [HttpGet]
    public async Task<IActionResult> Index(string name)
    {
        var products = await _service.GetAll(name);
        return View(products);
    }

    public async Task<IActionResult> Add()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Add(PantryProductModel pantryModel)
    {
        if (!ModelState.IsValid)
        {
            return View(pantryModel);
        }

        await _service.Add(pantryModel);
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Update(int id)
    {
        var product = await _service.GetById(id);
        if (product == null) return View("Error");
        var entity = new PantryProductModel()
        {
            Id = product.Id,
            Name = product.Name,
            ExpirationDate = product.ExpirationDate,
            PantryCategoryEnum = product.PantryCategoryEnum
        };
        return View(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Update(int id, PantryProductModel pantryModel)
    {
        if (id != pantryModel.Id) return View("Error");
        if (!ModelState.IsValid) return View(pantryModel);
        await _service.Update(pantryModel);
        return RedirectToAction(nameof(Index));
    }
    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var product = await _service.GetById(id);
        if (product == null) return View("Error");
        return View(product);
    }

    public async Task<IActionResult> DeleteNow(int id)
    {
        var entity = await _service.GetById(id);
        if (entity == null) return View("Error");
        await _service.Delete(id);
        return RedirectToAction(nameof(Index));

    }

}