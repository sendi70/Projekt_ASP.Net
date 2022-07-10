using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SaveToDateApp.Database;
using SaveToDateApp.Entities;
using SaveToDateApp.Models;

namespace SaveToDateApp.Services;

public class PantryProductsService:IPantryProductsService
{
    private readonly AppDbContext _dbContext;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly UserManager<IdentityUser> _userManager;

    public PantryProductsService(AppDbContext dbContext, IHttpContextAccessor accessor, UserManager<IdentityUser> userManager)
    {
        _dbContext = dbContext;
        _httpContextAccessor = accessor;
        _userManager = userManager;
    }
    
    public async Task Add(PantryProductModel pantryProduct)
    {
        var currentUser = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);

        var entity = new PantryProductEntity()
        {
            Name = pantryProduct.Name,
            ExpirationDate = pantryProduct.ExpirationDate,
            PantryCategoryEnum= pantryProduct.PantryCategoryEnum,
            Owner = currentUser,
        };

        await _dbContext.PantryProducts.AddAsync(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task Update(PantryProductModel pantryProduct)
    {
        var entity =  await _dbContext.PantryProducts.FirstOrDefaultAsync(e => e.Id == pantryProduct.Id);
        if (entity != null)
        {
            entity.Name = pantryProduct.Name;
            entity.ExpirationDate = pantryProduct.ExpirationDate;
            entity.PantryCategoryEnum = pantryProduct.PantryCategoryEnum;

        }
        await _dbContext.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
        var entity = await _dbContext.PantryProducts.FirstOrDefaultAsync(e => e.Id == id);
        _dbContext.PantryProducts.Remove(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<PantryProductEntity>> GetAll(string name)
    {
        var currentUser = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);

        IQueryable<PantryProductEntity> productsQuery = _dbContext.PantryProducts;
        productsQuery = productsQuery.Where(x => x.Owner == currentUser);

        if (!string.IsNullOrEmpty(name))
        {
            productsQuery = productsQuery.Where(x => x.Name.Contains(name));
        }

        var products = await productsQuery.ToListAsync();
        return products;
    }

    public async Task<PantryProductEntity> GetById(int id)
    {
        var currentUser = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
        var entity = await _dbContext.PantryProducts.FirstOrDefaultAsync(e => e.Id == id);
        return entity;
    }
}