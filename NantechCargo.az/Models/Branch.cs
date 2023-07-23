using System;
using System.Collections.Generic;

#nullable disable

namespace NantechCargo.az.Models
{
    public partial class Branch
    {
        public Branch()
        {
            Orders = new HashSet<Order>();
        }

        public int BranchId { get; set; }
        public string BranchName { get; set; }
        public string BranchAddress { get; set; }
        public string BranchPhone { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
