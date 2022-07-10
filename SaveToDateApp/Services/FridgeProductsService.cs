using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SaveToDateApp.Database;
using SaveToDateApp.Entities;
using SaveToDateApp.Models;

namespace SaveToDateApp.Services;

public class FridgeProductsService:IFridgeProductsService
{
    private readonly AppDbContext _dbContext;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly UserManager<IdentityUser> _userManager;

    public FridgeProductsService(AppDbContext dbContext, IHttpContextAccessor accessor, UserManager<IdentityUser> userManager)
    {
        _dbContext = dbContext;
        _httpContextAccessor = accessor;
        _userManager = userManager;
    }
    
    public async Task Add(FridgeProductModel fridgeProduct)
    {
        var currentUser = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);

        var entity = new FridgeProductEntity()
        {
            Name = fridgeProduct.Name,
            ExpirationDate = fridgeProduct.ExpirationDate,
            FridgeCategoryEnum= fridgeProduct.FridgeCategoryEnum,
            Owner = currentUser,
        };

        await _dbContext.FridgeProducts.AddAsync(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task Update(FridgeProductModel fridgeProduct)
    {
        var entity =  await _dbContext.FridgeProducts.FirstOrDefaultAsync(e => e.Id == fridgeProduct.Id);
        if (entity != null)
        {
            entity.Name = fridgeProduct.Name;
            entity.ExpirationDate = fridgeProduct.ExpirationDate;
            entity.FridgeCategoryEnum = fridgeProduct.FridgeCategoryEnum;

        }
        await _dbContext.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
        var entity = await _dbContext.FridgeProducts.FirstOrDefaultAsync(e => e.Id == id);
        _dbContext.FridgeProducts.Remove(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<FridgeProductEntity>> GetAll(string name)
    {
        var currentUser = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);

        IQueryable<FridgeProductEntity> productsQuery = _dbContext.FridgeProducts;
        productsQuery = productsQuery.Where(x => x.Owner == currentUser);

        if (!string.IsNullOrEmpty(name))
        {
            productsQuery = productsQuery.Where(x => x.Name.Contains(name));
        }

        var products = await productsQuery.ToListAsync();
        return products;
    }

    public async Task<FridgeProductEntity> GetById(int id)
    {
        var currentUser = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
        var entity = await _dbContext.FridgeProducts.FirstOrDefaultAsync(e => e.Id == id && e.Owner==currentUser);
        return entity;
    }
}