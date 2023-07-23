using System;
using System.Collections.Generic;

#nullable disable

namespace NantechCargo.az.Models
{
    public partial class Order
    {
        public Order()
        {
            Notifications = new HashSet<Notification>();
            Products = new HashSet<Product>();
        }

        public int OrderId { get; set; }
        public int? OrderClientId { get; set; }
        public int? OrderLevelId { get; set; }
        public int? OrderBranchId { get; set; }
        public DateTime OrderDate { get; set; }

        public virtual Branch OrderBranch { get; set; }
        public virtual User OrderClient { get; set; }
        public virtual Level OrderLevel { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
