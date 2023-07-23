using System;
using System.Collections.Generic;

#nullable disable

namespace NantechCargo.az.Models
{
    public partial class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductUrl { get; set; }
        public decimal? ProductPrice { get; set; }
        public decimal? ProductCargoAmount { get; set; }
        public int? ProductCount { get; set; }
        public int? ProductShopId { get; set; }
        public int? ProductOrderId { get; set; }
        public string ProductConfirm { get; set; }

        public virtual Order ProductOrder { get; set; }
    }
}
