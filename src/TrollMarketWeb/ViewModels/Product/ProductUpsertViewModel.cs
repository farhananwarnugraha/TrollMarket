using System.ComponentModel.DataAnnotations;

namespace TrollMarketWeb.ViewModels.Product;

public class ProductUpsertViewModel
{
    public int Productid { get; set; }
    [Required]
    public string? ProductName { get; set; }
    [Required]
    public string? CatgoryName { get; set; }
    public decimal? Price { get; set; }
    public string? DescriptionProduct { get; set; }
    public bool Discontiue { get; set; } 
}
