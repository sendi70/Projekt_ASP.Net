using SaveToDateApp.Entities;
using SaveToDateApp.Models;

namespace SaveToDateApp.Services;

public interface IFridgeProductsService
{
    Task Add(FridgeProductModel fridgeProduct);
    Task Update(FridgeProductModel fridgeProduct);
    Task Delete(int id);
    Task<IEnumerable<FridgeProductEntity>> GetAll(string name);
    Task<FridgeProductEntity> GetById(int id);
}