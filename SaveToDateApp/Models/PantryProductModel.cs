using System.ComponentModel.DataAnnotations;
using Microsoft.VisualBasic.CompilerServices;
using SaveToDateApp.Enum;

namespace SaveToDateApp.Models;

public class PantryProductModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    [Display(Name = "Expiration Date")]
    public DateTime ExpirationDate { get; set; }
    [Display(Name = "Category")]
    public PantryCategoryEnum PantryCategoryEnum { get; set; }
}