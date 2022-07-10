using SaveToDateApp.Entities;
using SaveToDateApp.Models;

namespace SaveToDateApp.Services;

public interface IPantryProductsService
{
    Task Add(PantryProductModel pantryProduct);
    Task Update(PantryProductModel pantryProduct);
    Task Delete(int id);
    Task<IEnumerable<PantryProductEntity>> GetAll(string name);
    Task<PantryProductEntity> GetById(int id);

}