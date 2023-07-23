using System;
using System.Collections.Generic;

#nullable disable

namespace NantechCargo.az.Models
{
    public partial class Level
    {
        public Level()
        {
            Orders = new HashSet<Order>();
        }

        public int LevelId { get; set; }
        public string LevelName { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
