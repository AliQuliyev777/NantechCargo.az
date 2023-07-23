using System;
using System.Collections.Generic;

#nullable disable

namespace NantechCargo.az.Models
{
    public partial class Category
    {
        public Category()
        {
            Mehsuls = new HashSet<Mehsul>();
        }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        public virtual ICollection<Mehsul> Mehsuls { get; set; }
    }
}
