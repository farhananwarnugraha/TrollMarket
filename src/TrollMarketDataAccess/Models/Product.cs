using System;
using System.Collections.Generic;

namespace TrollMarketDataAccess.Models
{
    public partial class Product
    {
        public Product()
        {
            OrderProducts = new HashSet<OrderProduct>();
        }

        public int Productid { get; set; }
        public string? ProductName { get; set; }
        public string? SellerUsername { get; set; }
        public string? CatgoryName { get; set; }
        public decimal? Price { get; set; }
        public string? DescriptionProduct { get; set; }
        public bool? Discontiue { get; set; }
        public bool? DeletedProduct { get; set; }

        public virtual User? SellerUsernameNavigation { get; set; }
        public virtual ICollection<OrderProduct> OrderProducts { get; set; }
    }
}
