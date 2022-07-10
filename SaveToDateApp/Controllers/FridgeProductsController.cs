using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SaveToDateApp.Models;
using SaveToDateApp.Services;

namespace SaveToDateApp.Controllers;
[Authorize]
public class FridgeProductsController : Controller
{
    private readonly IFridgeProductsService _service;

    public FridgeProductsController(IFridgeProductsService service)
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
    public async Task<IActionResult> Add(FridgeProductModel fridgeModel)
    {
        if (!ModelState.IsValid)
        {
            return View(fridgeModel);
        }

        await _service.Add(fridgeModel);
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Update(int id)
    {
        var product = await _service.GetById(id);
        if (product == null) return View("Error");
        var entity = new FridgeProductModel()
        {
            Id = product.Id,
            Name = product.Name,
            ExpirationDate = product.ExpirationDate,
            FridgeCategoryEnum = product.FridgeCategoryEnum
        };
        return View(entity);
    }
    
    [HttpPost]
    public async Task<IActionResult> Update(int id, FridgeProductModel fridgeModel)
    {
        if (id != fridgeModel.Id) return View("Error");
        if (!ModelState.IsValid) return View(fridgeModel);
        await _service.Update(fridgeModel);
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