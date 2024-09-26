using System;
using System.Collections.Generic;

namespace TrollMarketDataAccess.Models
{
    public partial class User
    {
        public User()
        {
            OrderProducts = new HashSet<OrderProduct>();
            Products = new HashSet<Product>();
        }

        public string Username { get; set; } = null!;
        public string? Password { get; set; }
        public string? FullName { get; set; }
        public string? Address { get; set; }
        public string Role { get; set; } = null!;
        public decimal? Balance { get; set; }

        public virtual ICollection<OrderProduct> OrderProducts { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
