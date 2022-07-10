using Microsoft.AspNetCore.Identity;
using Microsoft.VisualBasic.CompilerServices;
using SaveToDateApp.Enum;

namespace SaveToDateApp.Entities;

public class FridgeProductEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime ExpirationDate { get; set; }
    public FridgeCategoryEnum FridgeCategoryEnum { get; set; }
    public IdentityUser Owner { get; set; }
}