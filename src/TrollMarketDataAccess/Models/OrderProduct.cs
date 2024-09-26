using System;
using System.Collections.Generic;

namespace TrollMarketDataAccess.Models
{
    public partial class OrderProduct
    {
        public int IdOrder { get; set; }
        public int? Productid { get; set; }
        public string? UsernameBuyer { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime? OrderDate { get; set; }
        public int? ShipperId { get; set; }

        public virtual Product? Product { get; set; }
        public virtual Shipper? Shipper { get; set; }
        public virtual User? UsernameBuyerNavigation { get; set; }
    }
}
