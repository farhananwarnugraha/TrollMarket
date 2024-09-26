using System;
using System.Collections.Generic;

namespace TrollMarketDataAccess.Models
{
    public partial class Shipper
    {
        public Shipper()
        {
            OrderProducts = new HashSet<OrderProduct>();
        }

        public int ShipperId { get; set; }
        public string ShipperName { get; set; } = null!;
        public decimal Price { get; set; }
        public bool? IsService { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual ICollection<OrderProduct> OrderProducts { get; set; }
    }
}
