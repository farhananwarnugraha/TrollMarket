namespace TrollMarketAPI;

public class ProducctDTO
{
    public int Productid { get; set; }
    public string? ProductName { get; set; }
    public string? SellerUsername { get; set; }
    public string? CatgoryName { get; set; }
    public decimal? Price { get; set; }
    public string? DescriptionProduct { get; set; }
    public bool? Discontiue { get; set; }
    public bool? DeletedProduct { get; set; }
}
